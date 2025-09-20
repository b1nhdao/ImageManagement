namespace ImageManagement.Api.Models.FileModels
{
    public class DocumentUploadResult
    {
        public string RelativeUrl { get; }
        public string GeneratedFileName { get; }
        public long Size { get; }
        public string OriginalFileName { get; }
        public string FileExtension { get; }

        public DocumentUploadResult(string relativeUrl, string generatedFileName, long size, string originalFileName, string fileExtension)
        {
            RelativeUrl = relativeUrl;
            GeneratedFileName = generatedFileName;
            Size = size;
            OriginalFileName = originalFileName;
            FileExtension = fileExtension;
        }
    }
}
