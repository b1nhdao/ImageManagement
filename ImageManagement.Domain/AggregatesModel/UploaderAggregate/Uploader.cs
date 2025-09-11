using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.UploaderAggregate
{
    public class Uploader : Entity, IAggregateRoot
    {
        public string UserName { get; private set; }

        public Uploader(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public Uploader()
        {
        }
    }
}
