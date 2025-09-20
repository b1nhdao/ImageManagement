using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Document?>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentByIdQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Document?> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _documentRepository.GetByIdAsync(request.Id);
        }
    }
}
