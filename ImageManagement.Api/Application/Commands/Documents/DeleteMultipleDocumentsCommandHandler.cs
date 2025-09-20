using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteMultipleDocumentsCommandHandler : IRequestHandler<DeleteMultipleDocumentsCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;

        public DeleteMultipleDocumentsCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
        }

        public async Task<bool> Handle(DeleteMultipleDocumentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var images = await _documentRepository.GetByIdsAsync(request.Ids);

                if (!images.Any())
                {
                    throw new Exception("Not Found");
                }

                _documentRepository.RemoveRange(images);

                foreach (var item in images)
                {
                    string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.Url.TrimStart('/'));

                    if (File.Exists(physicalPath))
                    {
                        File.Delete(physicalPath);
                    }
                }

                return await _documentRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
