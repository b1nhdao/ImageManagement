using System.Text.RegularExpressions;
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
            var result = await _imageService.UploadAsync(request.File, request.UploaderId, cancellationToken);

            var image = new Image(
                id: Guid.NewGuid(),
                imageUrl: result.RelativeUrl,
                imageName: result.OriginalFileName,
                size: result.Size,
                uploadedTime: DateTime.UtcNow,
                uploaderId: request.UploaderId
            );

            _imageRepository.UploadImage(image);
            await _imageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return image;
        }
    }
}