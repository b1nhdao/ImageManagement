using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
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
        public Uploader Uploader { get; }



        public Image(Guid id, string imageUrl, string imageName, ImageSize size, DateTime uploadedTime, Guid uploaderId)
        {
            Id = id;
            ImageUrl = imageUrl;
            ImageName = imageName;
            Size = size;
            UploadedTime = uploadedTime;
            UploaderId = uploaderId;
        }

        public Image()
        {
        }

        public static Image Add(Guid id, string imageName, string imageUrl, ImageSize size, DateTime uploadedTime, Guid uploaderId)
        {
            if (uploaderId == Guid.Empty)
            {
                throw new Exception("Upload user Id is required");
            }

            if (imageUrl is null)
            {
                throw new Exception("Image URL is required");
            }

            Image image = new Image(id, imageName, imageUrl, size, uploadedTime, uploaderId);
            return image;
        }
    }
}
