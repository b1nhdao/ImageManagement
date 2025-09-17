using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommand : IRequest<Image>
    {
        public IFormFile Files { get; set; }
        public Guid UploaderId { get; set; }
        public ImageType ImageType { get; set; }

        public UploadImageCommand(IFormFile files, Guid uploaderId, ImageType imageType)
        {
            Files = files;
            UploaderId = uploaderId;
            ImageType = imageType;
        }
    }
}
