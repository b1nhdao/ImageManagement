using Ardalis.Result;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteMultipleDocumentsCommandHandler : IRequestHandler<DeleteMultipleDocumentsCommand, Result>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentService _documentService;

        public DeleteMultipleDocumentsCommandHandler(IDocumentRepository documentRepository, IDocumentService documentService)
        {
            _documentRepository = documentRepository;
            _documentService = documentService;
        }

        public async Task<Result> Handle(DeleteMultipleDocumentsCommand request, CancellationToken cancellationToken)
        {
            var images = await _documentRepository.GetByIdsAsync(request.Ids);

            if (images.Count() < request.Ids.Count())
            {
                return Result.NotFound("One of the uploaded Ids are not found");
            }

            try
            {
                _documentRepository.RemoveRange(images);
            }
            catch (Exception ex)
            {
                return Result.Error("Error when procesing with database: " + ex);
            }

            try
            {
                foreach (var item in images)
                {
                    string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.Url.TrimStart('/'));

                    if (File.Exists(physicalPath))
                    {
                        File.Delete(physicalPath);
                    }
                }
            }
            catch (Exception ex)
            {
                return Result.Error("Error when saving files: " + ex);
            }

            try
            {
                await _documentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Error("Error when procesing with database: " + ex);
            }

            return Result.SuccessWithMessage("Documents deleted successfully");
        }
    }
}
