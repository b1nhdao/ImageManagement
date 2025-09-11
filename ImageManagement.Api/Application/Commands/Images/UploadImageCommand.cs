using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommand : IRequest<Image>
    {
        public IFormFile Files { get; set; }
        public Guid UploaderId { get; set; }

        public UploadImageCommand(IFormFile files, Guid uploaderId)
        {
            Files = files;
            UploaderId = uploaderId;
        }
    }
}
