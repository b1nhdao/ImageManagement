using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.UploaderAggregate
{
    public interface IUploaderRepository : IRepository
    {
        Uploader Add (Uploader uploader);
        Task<(IEnumerable<Uploader>, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, bool isDescending, string keyword);
        Task<Uploader?> GetByIdAsync(int id);
    }
}
