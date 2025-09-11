using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Image>
    {
        public Task<Image> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
