using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public interface IImageRepository : IRepository
    {
        Image UploadImage(Image image);
        Task<Image?> GetImageByIdAsync(Guid id);
        IEnumerable<Image> UploadMultipleImages(IEnumerable<Image> images);
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task<(IEnumerable<Image>, int TotalCount)> GetPagedImagesAsync(int pageIndex, int pageSize, bool isDescending);
        Task<(IEnumerable<Image>, int TotalCount)> GetPagedImagesByUploaderIdAsync(Guid uploaderId, int pageIndex, int pageSize, bool isDescending);
        void DeleteImage(Image image);
        void DeleteMultipleImages(IEnumerable<Image> images);
        Task<IEnumerable<Image>> GetImagesByListIds(IEnumerable<Guid> ids);
    }
}
