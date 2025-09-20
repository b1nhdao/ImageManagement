using ImageManagement.Domain.AggregatesModel.ImageAggregate;

namespace ImageManagement.Api.Models.ImageModels
{
    public class ImageUploadResult
    {
        public string RelativeUrl { get; }
        public string GeneratedFileName { get; }
        public ImageDemensions Demensions { get; }
        public long Size { get; }
        public string OriginalFileName { get; }

        public ImageUploadResult(string relativeUrl, string generatedFileName, ImageDemensions demensions, long size, string originalFileName)
        {
            RelativeUrl = relativeUrl;
            GeneratedFileName = generatedFileName;
            Demensions = demensions;
            Size = size;
            OriginalFileName = originalFileName;
        }
    }
}
