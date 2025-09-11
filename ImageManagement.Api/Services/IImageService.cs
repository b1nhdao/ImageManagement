using ImageManagement.Domain.AggregatesModel.ImageAggregate;

namespace ImageManagement.Api.Services
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(IFormFile file, Guid uploaderId, CancellationToken cancellationToken = default);
    }

    public sealed class ImageUploadResult
    {
        public string RelativeUrl { get; }
        public string PhysicalPath { get; }
        public string GeneratedFileName { get; }
        public ImageSize Size { get; }
        public string OriginalFileName { get; }

        public ImageUploadResult(string relativeUrl, string physicalPath, string generatedFileName, ImageSize size, string originalFileName)
        {
            RelativeUrl = relativeUrl;
            PhysicalPath = physicalPath;
            GeneratedFileName = generatedFileName;
            Size = size;
            OriginalFileName = originalFileName;
        }
    }
}
