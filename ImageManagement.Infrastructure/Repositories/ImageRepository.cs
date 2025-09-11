using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using ImageManagement.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ImageManagement.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ImageDbContext _context;

        public ImageRepository(ImageDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Image>> GetAllImagesAsync()
        {
            return await _context.Images.AsNoTracking().ToListAsync();
        }

        public async Task<(IEnumerable<Image>, int TotalCount)> GetPagedImagesByUploaderIdAsync(Guid uploaderId, int pageIndex, int pageSize, bool isDescending)
        {
            var query = _context.Images.AsQueryable()
                .Where(i => i.UploaderId == uploaderId)
;
            int count = await query.CountAsync();

            query = isDescending ? query
                .OrderBy(i => i.UploadedTime) : query.OrderByDescending(i => i.UploadedTime);

            var item = await query.Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
            return (item, count);
        }

        public Image UploadImage(Image image)
        {
            _context.Add(image);
            return image;
        }

        public async Task<Image?> GetImageByIdAsync(Guid id)
        {
            return await _context.Images.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<(IEnumerable<Image>, int TotalCount)> GetPagedImagesAsync(int pageIndex, int pageSize, bool isDescending)
        {
            var query = _context.Images.AsQueryable();

            int count = await query.CountAsync();

            query = isDescending ? query.OrderBy(i => i.UploadedTime) : query.OrderByDescending(i => i.UploadedTime);

            var item = await query.Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();

            return (item, count);
        }

        public async Task<bool> DeleteImageByIdAsync(Guid id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            if(image is null)
            {
                return false;
            }

            _context.Images.Remove(image);
            var affected = await _context.SaveChangesAsync();

            return affected > 0;
        }
    }
}
