namespace ImageManagement.Api.DTOs
{
    public class AddImagesDTO
    {
        public IEnumerable<IFormFile> Files { get; set; }
        public Guid UploaderId { get; set; }
        public string FolderFileKey { get; set; } = string.Empty;

    }
}
