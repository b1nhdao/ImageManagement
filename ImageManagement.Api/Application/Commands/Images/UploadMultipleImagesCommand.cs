using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadMultipleImagesCommand : IRequest<IEnumerable<Image>>
    {
        public IEnumerable<IFormFile> Images { get; set; } = [];
        public Guid UploaderId { get; set; }
        public ImageType ImageType { get; set; }

        public UploadMultipleImagesCommand(IEnumerable<IFormFile> images, Guid uploaderId, ImageType imageType)
        {
            Images = images;
            UploaderId = uploaderId;
            ImageType = imageType;
        }
    }
}
