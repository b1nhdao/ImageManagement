using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class DeleteMultipleImagesCommandHandler : IRequestHandler<DeleteMultipleImagesCommand, bool>
    {
        private readonly IImageRepository _imageRepository;

        public DeleteMultipleImagesCommandHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<bool> Handle(DeleteMultipleImagesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _imageRepository.DeleteMultipleImages(request.Images);
                
                foreach (var item in request.Images)
                {
                    string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.ImageUrl.TrimStart('/'));

                    if (File.Exists(physicalPath))
                    {
                        File.Delete(physicalPath);
                    }
                }

                return await _imageRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
