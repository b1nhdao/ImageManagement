using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands
{
    public class UploadImageCommand : IRequest<Image>
    {
        public IFormFile File { get; set; }

        public UploadImageCommand(IFormFile file)
        {
            File = file;
        }
    }
}
