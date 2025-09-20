using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, Document>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;

        public UploadDocumentCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
        }

        public async Task<Document> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var result = await _documentService.UploadDocumentAsync(request.File, request.UploaderId, request.FolderFileKey, cancellationToken);

            var document = new Document(result.RelativeUrl, result.OriginalFileName, result.Size, DateTime.UtcNow, request.UploaderId);

            _documentRepository.Add(document);
            await _documentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return document;
        }
    }
}
