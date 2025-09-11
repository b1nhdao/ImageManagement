using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommand : IRequest<Image>
    {
        public IFormFile File { get; set; }
        public Guid UploaderId { get; set; }

        public UploadImageCommand(IFormFile file, Guid uploaderId)
        {
            File = file;
            UploaderId = uploaderId;
        }
    }
}
