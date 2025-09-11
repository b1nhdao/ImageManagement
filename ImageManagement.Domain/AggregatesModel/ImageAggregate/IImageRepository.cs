using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public interface IImageRepository : IRepository
    {
        Image UploadImage(Image image);
        Task<Image?> GetUploadImageByIdAsync(Guid id);
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task<IEnumerable<Image>> GetImagesByUploaderIdAsync(Guid uploaderId);
    }
}
