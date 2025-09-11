using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.UploaderAggregate
{
    public interface IUploaderRepository : IRepository
    {
        Task<Uploader?> GetUploaderByIdAsync(Guid id);
    }
}
