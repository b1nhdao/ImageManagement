using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Infrastructure.Repositories
{
    public class UploaderRepository : IUploaderRepository
    {
        private readonly ImageDbContext _context;

        public UploaderRepository(ImageDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Uploader?> GetUploaderByIdAsync(Guid id)
        {
            return await _context.Uploaders.FindAsync(id);
        }
    }
}
