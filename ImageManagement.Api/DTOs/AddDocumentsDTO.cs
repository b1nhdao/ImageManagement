namespace ImageManagement.Api.DTOs
{
    public class AddDocumentsDTO
    {
        public IEnumerable<IFormFile> Files { get; set; }
        public int UploaderId { get; set; }
        public string FolderFileKey { get; set; } = string.Empty;
    }
}
