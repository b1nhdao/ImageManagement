using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.UploaderAggregate
{
    public interface IUploaderRepository : IRepository
    {
        Uploader AddUploader (Uploader uploader);
        Task<(IEnumerable<Uploader>, int TotalCount)> GetPagedUploaderAsync(int pageIndex, int pageSize, bool isDescending, string keyword);
        Task<Uploader?> GetUploaderByIdAsync(Guid id);
    }
}
