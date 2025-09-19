using ImageManagement.Api.Extensions;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Users
{
    public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, PaginationResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetPagedUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginationResponse<User>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PaginationRequest.PageIndex;
            int pageSize = request.PaginationRequest.PageSize;
            bool isDescending = request.PaginationRequest.IsDescending;
            string keyWord = request.PaginationRequest.KeyWord.ToUnsign();

            var (items, totalCount) = await _userRepository.GetPagedUserAsync(pageIndex, pageSize, isDescending, keyWord);

            return new PaginationResponse<User>(pageIndex, pageSize, totalCount, items);
        }
    }
}
