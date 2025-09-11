using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Uploaders
{
    public class AddUploaderCommand : IRequest<Uploader>
    {
        public Uploader Uploader { get; set; }

        public AddUploaderCommand(Uploader uploader)
        {
            this.Uploader = uploader;
        }
    }
}
