using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Infrastructure.Repositories
{
    public class ImageRepository : BaseFileRepository<Image>, IImageRepository
    {
        private readonly AppDbContext _context;

        public ImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
