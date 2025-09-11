using ImageManagement.Domain.DomainEvents;
using MediatR;

namespace ImageManagement.Api.Application.DomainEventsHandler
{
    public class ImageAddedDomainEventHandler : INotificationHandler<ImageAddedDomainEvent>
    {
        private readonly ILogger<ImageAddedDomainEventHandler> _logger;

        public ImageAddedDomainEventHandler(ILogger<ImageAddedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(ImageAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("user {uid} added image {iid} at {dt}", notification.UploaderId, notification.Id, DateTime.Now);
        }
    }
}
