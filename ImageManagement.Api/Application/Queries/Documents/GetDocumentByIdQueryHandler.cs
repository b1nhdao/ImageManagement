using Ardalis.Result;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Result<Document>>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentByIdQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Result<Document>> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetByIdAsync(request.Id);
            
            if (document is null)
            {
                return Result.NotFound($"Document with id {request.Id} not found");
            }
            return Result.Success(document);
        }
    }
}
