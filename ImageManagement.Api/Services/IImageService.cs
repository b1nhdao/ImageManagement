using ImageManagement.Api.Models.ImageModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;

namespace ImageManagement.Api.Services
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(IFormFile file, Guid uploaderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ImageUploadResult>> UploadMultipleAsync(IEnumerable<IFormFile> files, Guid uploaderId, CancellationToken cancellationToken = default);
    }
}
