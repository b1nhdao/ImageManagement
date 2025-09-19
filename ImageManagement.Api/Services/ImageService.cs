using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ImageManagement.Api.Models.ImageModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageManagement.Api.Services
{
    public class ImageService : IImageService
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ImageService(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ImageUploadResult>> UploadMultipleImagesAsync(IEnumerable<IFormFile> files, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files), "Files collection cannot be null");

            var fileList = files.ToList();
            if (fileList.Count == 0)
                return [];

            var results = new List<ImageUploadResult>();
            var folder = FolderFactory.CreateFolder(folderTypeKey);

            var uploadedPath = new List<string>();

            foreach (var file in fileList)
            {
                try
                {
                    var imageUploadResult = await ConstructImageUploadResult(file, folder, cancellationToken);
                    results.Add(imageUploadResult);
                    uploadedPath.Add(imageUploadResult.RelativeUrl);
                }
                catch (Exception ex)
                {
                    // delete all uploaded files if exception occurs
                    if (uploadedPath.Any())
                    {
                        await DeleteMultipleImagesAsync(uploadedPath, cancellationToken);
                    }

                    throw new InvalidOperationException($"Failed to upload file '{file?.FileName}': {ex.Message}", ex);
                }
            }

            return results;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default)
        {
            var folder = FolderFactory.CreateFolder(folderTypeKey);
            return await ConstructImageUploadResult(file, folder, cancellationToken);
        }

        private static IFormFile ValidateFile(IFormFile file)
        {
            return file ?? throw new ArgumentNullException(nameof(file), "File cannot be null");
        }

        public async Task DeleteMultipleImagesAsync(IEnumerable<string> paths, CancellationToken cancellationToken)
        {
            foreach(var path in paths)
            {
                string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));

                if (File.Exists(physicalPath))
                {
                    await Task.Run(() => File.Delete(physicalPath), cancellationToken);
                }
            }
        }

        public async Task DeleteImageAsync(string path, CancellationToken cancellationToken)
        {
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path), cancellationToken);
            }
        }

        private static string GetValidatedExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                throw new NotSupportedException($"Unsupported file extension: {extension}. " +
                    $"Allowed extensions: {string.Join(", ", AllowedExtensions)}");
            }
            return extension;
        }

        private string CreateUploadDirectory(BaseFolder folder)
        {
            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadDirectory = Path.Combine(webRoot, "uploads", "images", folder.TargetFolder);
            Directory.CreateDirectory(uploadDirectory);
            return uploadDirectory;
        }

        private static string GenerateFileName(string originalFileName, string extension)
        {
            var datePrefix = DateTime.Now.ToString("ddMMyyHHmmss");
            var baseName = GetSafeFileName(originalFileName);
            return $"{datePrefix}_{baseName}{extension}";
        }

        private static string GetSafeFileName(string originalFileName)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            if (string.IsNullOrWhiteSpace(nameWithoutExtension))
            {
                nameWithoutExtension = "image";
            }
            var safeName = Regex.Replace(nameWithoutExtension, @"[^\w\-_]", "");
            return string.IsNullOrEmpty(safeName) ? "image" : safeName;
        }

        private static async Task SaveFileAsync(IFormFile file, string fullPath, CancellationToken cancellationToken)
        {
            await using var fileStream = new FileStream(fullPath, FileMode.CreateNew);
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        private static async Task<ImageDemensions> GetImageDimensionsAsync(string filePath, CancellationToken cancellationToken)
        {
            await using var readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(readStream, cancellationToken);
            return new ImageDemensions(image.Height, image.Width);
        }

        private long GetImageSizeByKbs(IFormFile file, CancellationToken cancellationToken)
        {            
            var imageSize = file.Length / 1024; // converting bytes to kb

            var maximumUploadSize = _configuration.GetValue<int>("ImageConfiguration:MaximumUploadPerFile");

            if (imageSize > maximumUploadSize)
            {
                throw new Exception($"File upload cant be higher than {maximumUploadSize}");
            }

            return imageSize;
        }

        private async Task<ImageUploadResult> ConstructImageUploadResult(IFormFile file, BaseFolder folder, CancellationToken cancellationToken)
        {
            var validFile = ValidateFile(file);
            var fileExtension = GetValidatedExtension(validFile);
            
            var imageSize = GetImageSizeByKbs(file, cancellationToken);

            var uploadDirectory = CreateUploadDirectory(folder);
            var generatedFileName = GenerateFileName(validFile.FileName, fileExtension);
            var fullPath = Path.Combine(uploadDirectory, generatedFileName);

            await SaveFileAsync(validFile, fullPath, cancellationToken);

            var imageDemensions = await GetImageDimensionsAsync(fullPath, cancellationToken);

            var relativeUrl = $"/uploads/images/{folder.TargetFolder}/{generatedFileName}";

            return new ImageUploadResult(
                relativeUrl,
                generatedFileName,
                imageDemensions,
                imageSize,
                validFile.FileName
            );
        }
    }
}
