using ImageManagement.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Users
{
    public class AddUserCommand : IRequest<User>
    {
        public User User { get; set; }

        public AddUserCommand(User User)
        {
            this.User = User;
        }
    }
}
