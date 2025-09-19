using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IRepository
    {
        User AddUser (User user);
        Task<(IEnumerable<User>, int TotalCount)> GetPagedUserAsync(int pageIndex, int pageSize, bool isDescending, string keyword);
        Task<User?> GetUserByIdAsync(int id);
    }
}
