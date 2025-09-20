using Ardalis.Result;
using ImageManagement.Api.Models.FileModels;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Infrastructure.Repositories;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, Result<Document>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;
        private readonly IUploaderRepository _uploaderRepository;

        public UploadDocumentCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService, IUploaderRepository uploaderRepository)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
            _uploaderRepository = uploaderRepository;
        }

        public async Task<Result<Document>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var exsistingUser = _uploaderRepository.GetByIdAsync(request.UploaderId);

            if(exsistingUser == null)
            {
                return Result.Error("User Not Found");
            }

            DocumentUploadResult result;

            try
            {
                result = await _documentService.UploadDocumentAsync(request.File, request.UploaderId, request.FolderFileKey, cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Error($"Error when saving files: please try again");
            }

            var document = new Document(result.RelativeUrl, result.OriginalFileName, result.Size, DateTime.UtcNow, request.UploaderId, Guid.NewGuid());

            try
            {
                _documentRepository.Add(document);
                await _documentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Error("Error when procesing with database, please try again");
            }

            return Result.Success(document, "Document uploaded successfully");
        }
    }
}
