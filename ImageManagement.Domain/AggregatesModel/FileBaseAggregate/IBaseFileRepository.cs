using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.FileBaseAggregate
{
    public interface IBaseFileRepository<TEntity> : IRepository where TEntity : BaseFile
    {
        TEntity Add(TEntity image);
        Task<TEntity?> GetByIdAsync(Guid id);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> images);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<(IEnumerable<TEntity>, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, bool isDescending);
        Task<(IEnumerable<TEntity>, int TotalCount)> GetPagedByUploaderIdAsync(int uploaderId, int pageIndex, int pageSize, bool isDescending);
        void Remove(TEntity image);
        void RemoveRange(IEnumerable<TEntity> images);
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
