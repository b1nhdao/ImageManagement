using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class UploadDocumentCommand : IRequest<Document>
    {
        public IFormFile File { get; set; }
        public int UploaderId { get; set; }
        public string FolderFileKey { get; set; } = string.Empty;

        public UploadDocumentCommand(IFormFile file, int uploaderId, string folderFileKey)
        {
            File = file;
            UploaderId = uploaderId;
            FolderFileKey = folderFileKey;
        }
    }
}
