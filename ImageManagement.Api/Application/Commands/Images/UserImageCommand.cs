using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class UserImageCommand : IRequest<Image>
    {
        public IFormFile Files { get; set; }
        public int UserId { get; set; }
        public string FolderTypeKey { get; set; }

        public UserImageCommand(IFormFile files, int userId, string folderTypeKey)
        {
            Files = files;
            UserId = userId;
            FolderTypeKey = folderTypeKey;
        }
    }
}
