namespace ImageManagement.Api.DTOs
{
    public class AddImageDTO
    {
        public IFormFile File {  get; set; }
        public Guid UploaderId { get; set; }
        public string FolderFileKey { get; set; } = string.Empty;
    }
}
