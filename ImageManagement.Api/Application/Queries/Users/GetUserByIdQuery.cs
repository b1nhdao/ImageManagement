using ImageManagement.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Users
{
    public class GetUserByIdQuery : IRequest<User?>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
