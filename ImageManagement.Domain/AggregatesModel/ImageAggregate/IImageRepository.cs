using ImageManagement.Domain.AggregatesModel.FileBaseAggregate;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public interface IImageRepository : IBaseFileRepository<Image>
    {
    }
}
