using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Api.Models.PaginationModels
{
    public class PaginationResponse<T> where T : Entity
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; } = [];
        public int TotalPages => TotalCount / PageSize;
        public bool HasPrevious => PageIndex < TotalPages;
        public bool HasNext => PageIndex > TotalPages;

        public PaginationResponse(int pageIndex, int pageSize, int totalCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }
    }
}
