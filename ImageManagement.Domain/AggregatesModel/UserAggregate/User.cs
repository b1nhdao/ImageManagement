using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public new int Id { get; private set; }
        public string UserName { get; private set; }

        public User(string userName) 
        {
            UserName = userName;
        }

        public User()
        {
        }
    }
}
