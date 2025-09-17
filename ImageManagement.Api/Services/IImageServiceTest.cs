using ImageManagement.Api.Models.ImageModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;

namespace ImageManagement.Api.Services
{
    public interface IImageServiceTest
    {
        Task<ImageUploadResult> UploadAsync(IFormFile file, Guid uploaderId, ImageType imageType, CancellationToken cancellationToken = default);
        Task<IEnumerable<ImageUploadResult>> UploadMultipleAsync(IEnumerable<IFormFile> files, Guid uploaderId, ImageType imageType, CancellationToken cancellationToken = default);
    }
}
