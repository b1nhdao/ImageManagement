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
            var results = await _imageService.UploadMultipleImagesAsync(request.Images, request.UploaderId, request.FolderTypeKey, cancellationToken);
            var images = new List<Image>();

            foreach (var result in results)
            {
                var image = new Image(
                    result.RelativeUrl,
                    result.OriginalFileName,
                    result.Demensions,
                    result.Size,
                    DateTime.UtcNow,
                    request.UploaderId
                );

                images.Add(image);
            }
            _imageRepository.AddRange(images);
            await _imageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return images;
        }
    }
}
