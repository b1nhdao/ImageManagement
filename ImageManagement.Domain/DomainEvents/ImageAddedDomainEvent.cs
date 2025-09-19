using MediatR;

namespace ImageManagement.Domain.DomainEvents
{
    public class ImageAddedDomainEvent : INotification
    {
        public Guid Id { get; set; }
        public int UploaderId { get; set; }

        public ImageAddedDomainEvent(Guid id, int uploaderId)
        {
            Id = id;
            UploaderId = uploaderId;
        }
    }
}
