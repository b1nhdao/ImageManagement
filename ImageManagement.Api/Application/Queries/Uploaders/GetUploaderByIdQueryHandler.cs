using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Uploaders
{
    public class GetUploaderByIdQueryHandler : IRequestHandler<GetUploaderByIdQuery, Uploader?>
    {
        private readonly IUploaderRepository _uploaderRepository;

        public GetUploaderByIdQueryHandler(IUploaderRepository uploaderRepository)
        {
            _uploaderRepository = uploaderRepository;
        }

        public async Task<Uploader?> Handle(GetUploaderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uploaderRepository.GetByIdAsync(request.Id);
        }
    }
}
