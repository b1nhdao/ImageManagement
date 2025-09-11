using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadMultipleImagesCommand : IRequest<IEnumerable<Image>>
    {
        public IEnumerable<IFormFile> Images { get; set; } = [];
        public Guid uploaderId { get; set; }

        public UploadMultipleImagesCommand(IEnumerable<IFormFile> images, Guid uploaderId)
        {
            Images = images;
            this.uploaderId = uploaderId;
        }
    }
}
