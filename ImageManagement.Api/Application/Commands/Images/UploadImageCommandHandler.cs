using System.Text.RegularExpressions;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Image>
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];

        private readonly IImageRepository _imageRepository;
        private readonly IWebHostEnvironment _env;

        public UploadImageCommandHandler(IImageRepository imageRepository, IWebHostEnvironment env)
        {
            _imageRepository = imageRepository;
            _env = env;
        }

        public async Task<Image> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var file = ValidateFile(request.File);
            var fileExtension = GetValidatedExtension(file);

            var uploadDirectory = CreateUploadDirectory();
            var fileName = GenerateFileName(file.FileName, fileExtension);
            var fullPath = Path.Combine(uploadDirectory, fileName);

            await SaveFileAsync(file, fullPath, cancellationToken);
            var imageSize = await GetImageDimensionsAsync(fullPath, cancellationToken);

            var relativeUrl = $"/uploads/images/{fileName}";

            var image =  new Image(
                Guid.NewGuid(),
                relativeUrl,
                file.FileName,
                imageSize,
                DateTime.UtcNow,
                request.UploaderId
            );

            _imageRepository.UploadImage(image);
            await _imageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return image;
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
            var datePrefix = DateTime.Now.ToString("ddMMyyyy");
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