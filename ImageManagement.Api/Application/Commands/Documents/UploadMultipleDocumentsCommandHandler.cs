using ImageManagement.Api.Services;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class UploadMultipleDocumentsCommandHandler : IRequestHandler<UploadMultipleDocumentsCommand, IEnumerable<Document>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;

        public UploadMultipleDocumentsCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
        }

        public async Task<IEnumerable<Document>> Handle(UploadMultipleDocumentsCommand request, CancellationToken cancellationToken)
        {
            var results = await _documentService.UploadMultipleDocumentsAsync(request.Documents, request.UploaderId, request.FolderTypeKey, cancellationToken);
            var images = new List<Document>();

            foreach (var result in results)
            {
                var image = new Document(
                    result.RelativeUrl,
                    result.OriginalFileName,
                    result.Size,
                    DateTime.UtcNow,
                    request.UploaderId
                );

                images.Add(image);
            }
            _documentRepository.AddRange(images);
            await _documentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return images;
        }
    }
}
