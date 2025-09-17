using System.Text.Json.Serialization;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Domain.DomainEvents;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class Image : Entity, IAggregateRoot
    {
        public string ImageUrl { get; private set; }
        public string ImageName { get; private set; }
        public ImageSize Size { get; private set; }
        public DateTime UploadedTime { get; private set; }
        public Guid UploaderId { get; private set; }
        [JsonIgnore]
        public Uploader Uploader { get; }

        public Image(string imageUrl, string imageName, ImageSize size, DateTime uploadedTime, Guid uploaderId) 
        {
            ImageUrl = imageUrl;
            ImageName = imageName;
            Size = size;
            UploadedTime = uploadedTime;
            UploaderId = uploaderId;

            AddDomainEvent(new ImageAddedDomainEvent(this.Id, uploaderId));
        }

        public static Image GetImage(Guid id, string imageUrl, string imageName, ImageSize size, DateTime uploadedTime, Guid uploaderId)
        {
            var image = new Image(imageUrl, imageName, size, uploadedTime, uploaderId);
            image.Id = id;

            return image;
        }

        public Image()
        {
        }
    }
}
