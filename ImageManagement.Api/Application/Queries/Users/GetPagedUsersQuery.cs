using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Users
{
    public class GetPagedUsersQuery : IRequest<PaginationResponse<User>>
    {
        public PaginationRequest PaginationRequest { get; set; }

        public GetPagedUsersQuery(PaginationRequest paginationRequest)
        {
            PaginationRequest = paginationRequest;
        }
    }
}
