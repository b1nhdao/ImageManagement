using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Uploaders
{
    public class AddUploaderCommandHandler : IRequestHandler<AddUploaderCommand, Uploader>
    {
        private readonly IUploaderRepository _uploaderRepository;

        public AddUploaderCommandHandler(IUploaderRepository uploaderRepository)
        {
            _uploaderRepository = uploaderRepository;
        }

        public async Task<Uploader> Handle(AddUploaderCommand request, CancellationToken cancellationToken)
        {
            var uploader = new Uploader(request.Uploader.UserName);

            uploader = _uploaderRepository.AddUploader(request.Uploader);

            await _uploaderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return uploader;
        }
    }
}
