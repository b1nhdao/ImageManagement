using ImageManagement.Domain.AggregatesModel.ImageAggregate;

namespace ImageManagement.Api.DTOs
{
    public class ImageDeleteDTO
    {
        public Guid Id { get; set; } 
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public ImageSize Size { get; set; }
        public DateTime UploadedTime { get; set; }
        public Guid UploaderId { get; set; }

        public ImageDeleteDTO(Guid id, string imageUrl, string imageName, ImageSize size, DateTime uploadedTime, Guid uploaderId)
        {
            Id = id;
            ImageUrl = imageUrl;
            ImageName = imageName;
            Size = size;
            UploadedTime = uploadedTime;
            UploaderId = uploaderId;
        }
    }
}
