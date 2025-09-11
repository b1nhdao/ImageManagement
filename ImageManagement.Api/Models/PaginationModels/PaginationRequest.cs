namespace ImageManagement.Api.Models.PaginationModels
{
    public class PaginationRequest
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public bool IsDescending { get; set; } = false;
        public string KeyWord { get; set; } = string.Empty;
    }
}
