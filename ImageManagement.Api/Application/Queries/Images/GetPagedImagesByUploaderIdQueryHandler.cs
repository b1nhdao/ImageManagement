using ImageManagement.Api.Models.Pagination;
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

            var (item, totalCount) = await _imageRepository.GetPagedImagesByUploaderIdAsync(request.UploaderId, pageIndex, pageSize, isDescending);

            return new PaginationResponse<Image>(pageIndex, pageSize, totalCount, item);
        }
    }
}
