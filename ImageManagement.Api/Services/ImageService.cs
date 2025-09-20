using System.Text.RegularExpressions;
using ImageManagement.Api.Models.ImageModels;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using ImageManagement.Domain.FolderType;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageManagement.Api.Services
{
    public class ImageService : IImageService
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png"];
        private readonly IFileStorageService _fileStorageService;

        public ImageService(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
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

        private static IFormFile ValidateFile(IFormFile file)
        {
            return file ?? throw new ArgumentNullException(nameof(file), "File cannot be null");
        }

        private static async Task<ImageDemensions> GetImageDimensionsAsync(string filePath, CancellationToken cancellationToken)
        {
            await using var readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(readStream, cancellationToken);

            return new ImageDemensions(image.Height, image.Width);
        }

        private async Task<ImageUploadResult> ConstructImageUploadResult(IFormFile file, BaseFolder folder, CancellationToken cancellationToken)
        {
            var validFile = FileValidationService.ValidateFile(file);
            var fileExtension = FileValidationService.GetValidatedImageExtension(validFile);

            var imageSize = _fileStorageService.ValidateAndGetFileSizeKb(file, "ImageConfiguration:MaximumUploadPerFile");

            var uploadDirectory = _fileStorageService.CreateUploadDirectory(folder, "images");
            var generatedFileName = _fileStorageService.GenerateFileName(validFile.FileName, fileExtension);
            var fullPath = Path.Combine(uploadDirectory, generatedFileName);

            await _fileStorageService.SaveFileAsync(validFile, fullPath, cancellationToken);

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
