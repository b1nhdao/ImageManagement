using ImageManagement.Api.Services;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Image>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;

        public UploadImageCommandHandler(IImageRepository imageRepository, IImageService imageService)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<Image> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _imageService.UploadImageAsync(request.Files, request.UploaderId, request.FolderTypeKey, cancellationToken);

            var image = new Image(
                result.RelativeUrl,
                result.OriginalFileName,
                result.Demensions,
                result.Size,
                DateTime.UtcNow,
                request.UploaderId
            );

            _imageRepository.Add(image);
            await _imageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return image;
        }
    }
}