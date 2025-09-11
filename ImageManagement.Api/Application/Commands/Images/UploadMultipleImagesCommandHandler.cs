using ImageManagement.Api.Services;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadMultipleImagesCommandHandler : IRequestHandler<UploadMultipleImagesCommand, IEnumerable<Image>>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;

        public UploadMultipleImagesCommandHandler(IImageRepository imageRepository, IImageService imageService)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<IEnumerable<Image>> Handle(UploadMultipleImagesCommand request, CancellationToken cancellationToken)
        {
            var results = await _imageService.UploadMultipleAsync(request.Images, request.uploaderId, cancellationToken);
            var images = new List<Image>();

            foreach (var result in results)
            {
                var image = new Image(
                    Guid.NewGuid(),
                    result.RelativeUrl,
                    result.OriginalFileName,
                    result.Size,
                    DateTime.UtcNow,
                    request.uploaderId
                );

                _imageRepository.UploadImage(image);
                images.Add(image);
            }
            await _imageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return images;
        }
    }
}
