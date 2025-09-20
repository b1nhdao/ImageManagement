using ImageManagement.Domain.AggregatesModel.FileBaseAggregate;
using ImageManagement.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ImageManagement.Infrastructure.Repositories
{
    public class BaseFileRepository<TEntity> : IBaseFileRepository<TEntity> where TEntity : BaseFile
    {
        private readonly AppDbContext _context;

        public BaseFileRepository(AppDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<(IEnumerable<TEntity>, int TotalCount)> GetPagedByUploaderIdAsync(int uploaderId, int pageIndex, int pageSize, bool isDescending, string keyword, DateOnly? fromDate = null, DateOnly? toDate = null)
        {
            var query = _context.Set<TEntity>().AsQueryable()
                .Where(i => i.UploaderId == uploaderId);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim().ToLower();
                query = query.Where(i => i.Name.ToLower().Contains(k));
            }

            if (fromDate.HasValue)
            {
                var fromDateTimeUtc = fromDate.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
                query = query.Where(i => i.UploadedTime >= fromDateTimeUtc);
            }

            if (toDate.HasValue)
            {
                var toDateTimeUtc = fromDate.Value.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);
                query = query.Where(i => i.UploadedTime <= toDateTimeUtc);
            }


            int count = await query.CountAsync();

            query = isDescending ? query
                .OrderBy(i => i.UploadedTime) : query.OrderByDescending(i => i.UploadedTime);

            var item = await query.Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
            return (item, count);
        }

        public TEntity Add(TEntity image)
        {
            _context.Add(image);
            return image;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<(IEnumerable<TEntity>, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, bool isDescending, string keyword, DateOnly? fromDate = null, DateOnly? toDate = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim().ToLower();
                query = query.Where(i => i.Name.ToLower().Contains(k));
            }

            if (fromDate.HasValue)
            {
                var fromDateTimeUtc = fromDate.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
                query = query.Where(i => i.UploadedTime >= fromDateTimeUtc);
            }

            if (toDate.HasValue)
            {
                var toDateTimeUtc = fromDate.Value.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);
                query = query.Where(i => i.UploadedTime <= toDateTimeUtc);
            }

            int count = await query.CountAsync();

            query = isDescending ? query.OrderBy(i => i.UploadedTime) : query.OrderByDescending(i => i.UploadedTime);

            var item = await query.Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();

            return (item, count);
        }

        public void Remove(TEntity image)
        {
            _context.Set<TEntity>().Remove(image);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> images)
        {
            _context.AddRange(images);
            return images;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Set<TEntity>().Where(i => ids.Contains(i.Id)).ToListAsync();
        }
    }
}
