namespace ImageManagement.Api.DTOs
{
    public class AddDocumentDTO
    {
        public IFormFile File { get; set; }
        public int UploaderId { get; set; }
        public string FolderFileKey { get; set; } = string.Empty;
    }
}
