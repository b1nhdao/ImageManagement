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
        public ImageDemensions Demension { get; private set; }
        public long Size { get; private set; } // in KB
        public DateTime UploadedTime { get; private set; }
        public int UploaderId { get; private set; }
        [JsonIgnore]
        public Uploader Uploader { get; }

        public Image(string imageUrl, string imageName, ImageDemensions imageDemension, long size, DateTime uploadedTime, int uploaderId)
        {
            Id = Guid.NewGuid();
            ImageUrl = imageUrl;
            ImageName = imageName;
            Demension = imageDemension;
            Size = size;
            UploadedTime = uploadedTime;
            UploaderId = uploaderId;

            AddDomainEvent(new ImageAddedDomainEvent(Id, uploaderId));
        }

        public Image()
        {
        }
    }
}
