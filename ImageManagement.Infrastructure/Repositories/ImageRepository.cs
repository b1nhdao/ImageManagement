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
            return await _context.Images.ToListAsync();
        }

        public async Task<IEnumerable<Image>> GetImagesByUploaderIdAsync(Guid uploaderId)
        {
            return await _context.Images.Where(i => i.UploaderId == uploaderId).ToListAsync();
        }

        public Image UploadImage(Image image)
        {
            _context.Add(image);
            return image;
        }

        public async Task<Image?> GetUploadImageByIdAsync(Guid id)
        {
            return await _context.Images.FindAsync(id);
        }
    }
}
