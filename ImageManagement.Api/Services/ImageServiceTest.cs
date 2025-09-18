using System.Text.RegularExpressions;
using ImageManagement.Api.Models.ImageModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageManagement.Api.Services
{
    public class ImageServiceTest : IImageServiceTest
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];
        private readonly IWebHostEnvironment _env;

        public ImageServiceTest(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<IEnumerable<ImageUploadResult>> UploadMultipleAsync(IEnumerable<IFormFile> files, Guid uploaderId, string folderTypeKey, CancellationToken cancellationToken = default)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files), "Files collection cannot be null");

            var fileList = files.ToList();
            if (fileList.Count == 0)
                return [];

            var results = new List<ImageUploadResult>();
            var folder = FolderFactory.CreateFolder(folderTypeKey);

            foreach (var file in fileList)
            {
                try
                {
                    var imageUploadResult = await ConstructImageUploadResult(file, folder, cancellationToken);
                    results.Add(imageUploadResult);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to upload file '{file?.FileName}': {ex.Message}", ex);
                }
            }

            return results;
        }

        public async Task<ImageUploadResult> UploadAsync(IFormFile file, Guid uploaderId, string folderTypeKey, CancellationToken cancellationToken = default)
        {
            var folder = FolderFactory.CreateFolder(folderTypeKey);
            return await ConstructImageUploadResult(file, folder, cancellationToken);
        }

        private static IFormFile ValidateFile(IFormFile file)
        {
            return file ?? throw new ArgumentNullException(nameof(file), "File cannot be null");
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

        private static async Task<ImageSize> GetImageDimensionsAsync(string filePath, CancellationToken cancellationToken)
        {
            await using var readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(readStream, cancellationToken);
            return new ImageSize(image.Height, image.Width);
        }

        private async Task<ImageUploadResult> ConstructImageUploadResult(IFormFile file, BaseFolder folder, CancellationToken cancellationToken)
        {
            var validFile = ValidateFile(file);
            var fileExtension = GetValidatedExtension(validFile);

            var uploadDirectory = CreateUploadDirectory(folder);
            var generatedFileName = GenerateFileName(validFile.FileName, fileExtension);
            var fullPath = Path.Combine(uploadDirectory, generatedFileName);

            await SaveFileAsync(validFile, fullPath, cancellationToken);
            var imageSize = await GetImageDimensionsAsync(fullPath, cancellationToken);

            var relativeUrl = $"/uploads/images/{folder.TargetFolder}/{generatedFileName}";

            return new ImageUploadResult(
                relativeUrl,
                generatedFileName,
                imageSize,
                validFile.FileName
            );
        }
    }
}
