using ImageManagement.Api.Models.ImageModels;

namespace ImageManagement.Api.Services
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(IFormFile file, Guid uploaderId, string folderTypeKey, CancellationToken cancellationToken = default);
        Task<IEnumerable<ImageUploadResult>> UploadMultipleAsync(IEnumerable<IFormFile> files, Guid uploaderId, string folderTypeKey, CancellationToken cancellationToken = default);
    }
}
