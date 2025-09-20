using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Images
{
    public class GetPagedImagesByUploaderIdQueryHandler : IRequestHandler<GetPagedImagesByUploaderIdQuery, PaginationResponse<Image>>
    {
        private readonly IImageRepository _imageRepository;

        public GetPagedImagesByUploaderIdQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<PaginationResponse<Image>> Handle(GetPagedImagesByUploaderIdQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PaginationRequest.PageIndex;
            int pageSize = request.PaginationRequest.PageSize;
            bool isDescending = request.PaginationRequest.IsDescending;
            string keyword = request.PaginationRequest.KeyWord;
            DateOnly? fromDate = request.PaginationRequest.FromDate;
            DateOnly? toDate = request.PaginationRequest.ToDate;

            var (item, totalCount) = await _imageRepository.GetPagedByUploaderIdAsync(request.UploaderId, pageIndex, pageSize, isDescending, keyword, fromDate, toDate);

            return new PaginationResponse<Image>(pageIndex, pageSize, totalCount, item);
        }
    }
}
