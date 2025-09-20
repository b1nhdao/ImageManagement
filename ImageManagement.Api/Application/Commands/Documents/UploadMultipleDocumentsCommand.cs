using Ardalis.Result;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class UploadMultipleDocumentsCommand : IRequest<Result<List<Document>>>
    {
        public IEnumerable<IFormFile> Documents { get; set; } = [];
        public int UploaderId { get; set; }
        public string FolderTypeKey { get; set; }

        public UploadMultipleDocumentsCommand(IEnumerable<IFormFile> documents, int uploaderId, string folderTypeKey)
        {
            Documents = documents;
            UploaderId = uploaderId;
            FolderTypeKey = folderTypeKey;
        }
    }
}
