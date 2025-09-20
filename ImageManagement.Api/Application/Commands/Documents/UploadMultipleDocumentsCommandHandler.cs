using Ardalis.Result;
using ImageManagement.Api.Models.FileModels;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class UploadMultipleDocumentsCommandHandler : IRequestHandler<UploadMultipleDocumentsCommand, Result<List<Document>>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;

        public UploadMultipleDocumentsCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
        }

        public async Task<Result<List<Document>>> Handle(UploadMultipleDocumentsCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<DocumentUploadResult> results;

            try
            {
                results = await _documentService.UploadMultipleDocumentsAsync(request.Documents, request.UploaderId, request.FolderTypeKey, cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Error($"Error when saving files: " + ex);
            }

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

            try
            {
                _documentRepository.AddRange(images);
            }
            catch (Exception ex)
            {
                return Result.Error("Error when procesing with database: " + ex);
            }
            await _documentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Result.Success(images, "Documents uploaded successfully");
        }
    }
}
