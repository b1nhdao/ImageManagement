using ImageManagement.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.User.UserName);

            user = _userRepository.AddUser(request.User);

            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return user;
        }
    }
}
