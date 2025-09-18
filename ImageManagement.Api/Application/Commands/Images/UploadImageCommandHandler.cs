using ImageManagement.Api.Services;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Image>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageServiceTest _imageService;

        public UploadImageCommandHandler(IImageRepository imageRepository, IImageServiceTest imageService)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<Image> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _imageService.UploadAsync(request.Files, request.UploaderId, request.FolderTypeKey, cancellationToken);

            var image = new Image(
                result.RelativeUrl,
                result.OriginalFileName,
                result.Size,
                DateTime.UtcNow,
                request.UploaderId
            );

            _imageRepository.UploadImage(image);
            await _imageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return image;
        }
    }
}