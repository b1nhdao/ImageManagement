using MediatR;

namespace ImageManagement.Domain.DomainEvents
{
    public class ImageAddedDomainEvent : INotification
    {
        public Guid Id { get; set; }
        public Guid UploaderId { get; set; }

        public ImageAddedDomainEvent(Guid id, Guid uploaderId)
        {
            Id = id;
            UploaderId = uploaderId;
        }
    }
}
