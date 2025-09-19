using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ImageManagement.Infrastructure.Repositories
{
    public class UploaderRepository : IUploaderRepository
    {
        private readonly AppDbContext _context;

        public UploaderRepository(AppDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Uploader Add(Uploader uploader)
        {
            _context.Add(uploader);
            return uploader;
        }

        public async Task<(IEnumerable<Uploader>, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, bool isDescending, string keyword)
        {
            var query = _context.Uploaders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(u => u.UserName.ToLower().Contains(keyword.ToLower()));
            }

            int count = await query.CountAsync();

            query = isDescending ? query.OrderByDescending(i => i.UserName) : query.OrderBy(i => i.UserName);

            var item = await query.Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();

            return (item, count);
        }

        public async Task<Uploader?> GetByIdAsync(int id)
        {
            return await _context.Uploaders.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
