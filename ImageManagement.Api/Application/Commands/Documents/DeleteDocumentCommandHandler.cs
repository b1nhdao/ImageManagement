using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;

        public DeleteDocumentCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
        }

        public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var documentExisting = await _documentRepository.GetByIdAsync(request.Id);
            if (documentExisting is null)
            {
                throw new ArgumentNullException("Not found");
            }

            _documentRepository.Remove(documentExisting);
            return await _documentRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
