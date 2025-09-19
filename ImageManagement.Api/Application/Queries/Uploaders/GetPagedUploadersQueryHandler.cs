using ImageManagement.Api.Extensions;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Uploaders
{
    public class GetPagedUploadersQueryHandler : IRequestHandler<GetPagedUploadersQuery, PaginationResponse<Uploader>>
    {
        private readonly IUploaderRepository _uploaderRepository;

        public GetPagedUploadersQueryHandler(IUploaderRepository uploaderRepository)
        {
            _uploaderRepository = uploaderRepository;
        }

        public async Task<PaginationResponse<Uploader>> Handle(GetPagedUploadersQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PaginationRequest.PageIndex;
            int pageSize = request.PaginationRequest.PageSize;
            bool isDescending = request.PaginationRequest.IsDescending;
            string keyWord = request.PaginationRequest.KeyWord.ToUnsign();

            var (items, totalCount) = await _uploaderRepository.GetPagedAsync(pageIndex, pageSize, isDescending, keyWord);

            return new PaginationResponse<Uploader>(pageIndex, pageSize, totalCount, items);
        }
    }
}
