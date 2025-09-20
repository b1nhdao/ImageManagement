using ImageManagement.Domain.FileBase;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public interface IImageRepository : IBaseFileRepository<Image>
    {
    }
}
