using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommand : IRequest<Image>
    {
        public IFormFile Files { get; set; }
        public Guid UploaderId { get; set; }
        public string FolderTypeKey { get; set; }

        public UploadImageCommand(IFormFile files, Guid uploaderId, string folderTypeKey)
        {
            Files = files;
            UploaderId = uploaderId;
            FolderTypeKey = folderTypeKey;
        }
    }
}
