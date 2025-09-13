using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, bool>
    {
        private readonly IImageRepository _imageRepository;

        public DeleteImageCommandHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", request.Image.ImageUrl.TrimStart('/'));

                _imageRepository.DeleteImage(request.Image);
                
                if (File.Exists(physicalPath))
                {
                    File.Delete(physicalPath);
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
