using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.UploaderAggregate
{
    public class Uploader : Entity, IAggregateRoot
    {
        public string UserName { get; private set; }

        public Uploader(string userName) 
        {
            UserName = userName;
        }

        public Uploader()
        {
        }
    }
}
