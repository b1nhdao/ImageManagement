using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadImageCommand : IRequest<Image>
    {
        public IFormFile Files { get; set; }
        public int UploaderId { get; set; }
        public string FolderTypeKey { get; set; } = string.Empty;

        public UploadImageCommand(IFormFile files, int uploaderId, string folderTypeKey)
        {
            Files = files;
            UploaderId = uploaderId;
            FolderTypeKey = folderTypeKey;
        }
    }
}
