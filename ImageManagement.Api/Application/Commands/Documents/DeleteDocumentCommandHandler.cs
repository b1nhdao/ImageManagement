using Ardalis.Result;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Result>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;

        public DeleteDocumentCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
        }

        public async Task<Result> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var documentExisting = await _documentRepository.GetByIdAsync(request.Id);
            if (documentExisting is null)
            {
                return Result.NotFound($"Document with id {request.Id} not found");
            }

            string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", documentExisting.Url.TrimStart('/'));

            try
            {
                _documentRepository.Remove(documentExisting);
            }
            catch (Exception ex)
            {
                return Result.Error("Error when procesing with database: " + ex);
            }
            await _documentService.DeleteDocumentAsync(physicalPath, cancellationToken);

            try
            {
                var result = await _documentRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;        
            }
            catch (Exception ex)
            {
                return Result.Error("Error when procesing with database: " + ex);
            }

            return Result.SuccessWithMessage("Document deleted succesfully");
        }
    }
}
