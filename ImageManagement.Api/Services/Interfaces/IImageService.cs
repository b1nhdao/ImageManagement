using ImageManagement.Api.Models.ImageModels;

namespace ImageManagement.Api.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default);
        Task<IEnumerable<ImageUploadResult>> UploadMultipleImagesAsync(IEnumerable<IFormFile> files, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default);
        Task DeleteImageAsync(string path, CancellationToken cancellationToken = default);
        Task DeleteMultipleImagesAsync(IEnumerable<string> paths, CancellationToken cancellationToken = default);
    }
}
