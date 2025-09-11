
using System.Text.RegularExpressions;
using ImageManagement.Api.Models.ImageModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageManagement.Api.Services
{
    public class ImageService : IImageService
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<IEnumerable<ImageUploadResult>> UploadMultipleAsync(IEnumerable<IFormFile> files, Guid uploaderId, CancellationToken cancellationToken = default)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files), "Files collection cannot be null");

            var fileList = files.ToList();
            if (!fileList.Any())
                return Enumerable.Empty<ImageUploadResult>();

            var results = new List<ImageUploadResult>();
            var uploadDirectory = CreateUploadDirectory();

            foreach (var file in fileList)
            {
                try
                {
                    var validFile = ValidateFile(file);
                    var fileExtension = GetValidatedExtension(validFile);
                    var generatedFileName = GenerateFileName(validFile.FileName, fileExtension);
                    var fullPath = Path.Combine(uploadDirectory, generatedFileName);

                    await SaveFileAsync(validFile, fullPath, cancellationToken);
                    var imageSize = await GetImageDimensionsAsync(fullPath, cancellationToken);

                    var relativeUrl = $"/uploads/images/{generatedFileName}";

                    results.Add(new ImageUploadResult(
                        relativeUrl: relativeUrl,
                        physicalPath: fullPath,
                        generatedFileName: generatedFileName,
                        size: imageSize,
                        originalFileName: validFile.FileName
                    ));
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to upload file '{file?.FileName}': {ex.Message}", ex);
                }
            }

            return results;
        }

        public async Task<ImageUploadResult> UploadAsync(IFormFile file, Guid uploaderId, CancellationToken cancellationToken = default)
        {
            var validFile = ValidateFile(file);
            var fileExtension = GetValidatedExtension(validFile);

            var uploadDirectory = CreateUploadDirectory();
            var generatedFileName = GenerateFileName(validFile.FileName, fileExtension);
            var fullPath = Path.Combine(uploadDirectory, generatedFileName);

            await SaveFileAsync(validFile, fullPath, cancellationToken);
            var imageSize = await GetImageDimensionsAsync(fullPath, cancellationToken);

            var relativeUrl = $"/uploads/images/{generatedFileName}";

            return new ImageUploadResult(
                relativeUrl: relativeUrl,
                physicalPath: fullPath,
                generatedFileName: generatedFileName,
                size: imageSize,
                originalFileName: validFile.FileName
            );
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

        private string CreateUploadDirectory()
        {
            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadDirectory = Path.Combine(webRoot, "uploads", "images");
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
    }
}
