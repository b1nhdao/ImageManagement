using ImageManagement.Domain.AggregatesModel.UserAggregate;
using ImageManagement.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ImageManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ImageDbContext _context;

        public UserRepository(ImageDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public User AddUser(User uploader)
        {
            _context.Add(uploader);
            return uploader;
        }

        public async Task<(IEnumerable<User>, int TotalCount)> GetPagedUserAsync(int pageIndex, int pageSize, bool isDescending, string keyword)
        {
            var query = _context.Users.AsQueryable();

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

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
