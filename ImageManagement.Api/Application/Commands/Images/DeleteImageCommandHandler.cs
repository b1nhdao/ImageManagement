using ImageManagement.Api.Services;
using ImageManagement.Domain.AggregatesModel.FileBaseAggregate;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, bool>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;

        public DeleteImageCommandHandler(IImageRepository imageRepository, IImageService imageService)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageExisting = await _imageRepository.GetByIdAsync(request.Id) ?? throw new ArgumentNullException($"Image with {request.Id} not found int the database");
                
                string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageExisting.Url.TrimStart('/'));

                try
                {
                    _imageRepository.Remove(imageExisting);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
                await _imageService.DeleteImageAsync(physicalPath, cancellationToken);

                return await _imageRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
