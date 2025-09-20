using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.FileBase
{
    public interface IBaseFileRepository<TEntity> : IRepository where TEntity : BaseFile
    {
        TEntity Add(TEntity image);
        Task<TEntity?> GetByIdAsync(Guid id);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> images);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<(IEnumerable<TEntity>, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, bool isDescending, string keyword, DateOnly? fromDate = null, DateOnly? toDate = null);
        Task<(IEnumerable<TEntity>, int TotalCount)> GetPagedByUploaderIdAsync(int uploaderId, int pageIndex, int pageSize, bool isDescending, string keyword, DateOnly? fromDate = null, DateOnly? toDate = null);
        void Remove(TEntity image);
        void RemoveRange(IEnumerable<TEntity> images);
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<IEnumerable<TEntity>> GetByFolderTypeId(Guid id);
    }
}
