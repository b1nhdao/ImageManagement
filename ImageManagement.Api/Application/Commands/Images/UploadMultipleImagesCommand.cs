using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UploadMultipleImagesCommand : IRequest<IEnumerable<Image>>
    {
        public IEnumerable<IFormFile> Images { get; set; } = [];
        public Guid UploaderId { get; set; }
        public string FolderTypeKey { get; set; }

        public UploadMultipleImagesCommand(IEnumerable<IFormFile> images, Guid uploaderId, string folderTypeKey)
        {
            Images = images;
            UploaderId = uploaderId;
            FolderTypeKey = folderTypeKey;
        }
    }
}
