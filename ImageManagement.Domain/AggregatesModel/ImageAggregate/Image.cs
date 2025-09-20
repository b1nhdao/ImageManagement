using ImageManagement.Domain.DomainEvents;
using ImageManagement.Domain.FileBase;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class Image : BaseFile, IAggregateRoot
    {
        public ImageDemensions Demension { get; private set; }

        public Image(string imageUrl, string imageName, ImageDemensions imageDemension, long size, DateTime uploadedTime, int uploaderId)
            : base(imageUrl, imageName, size, uploadedTime, uploaderId)
        {
            Id = Guid.NewGuid();
            Demension = imageDemension;

            AddDomainEvent(new ImageAddedDomainEvent(Id, uploaderId));
        }

        public Image() : base()
        {
        }
    }
}
