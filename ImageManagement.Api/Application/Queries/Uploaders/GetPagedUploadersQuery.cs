using ImageManagement.Api.Models.Pagination;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Uploaders
{
    public class GetPagedUploadersQuery : IRequest<PaginationResponse<Uploader>>
    {
        public PaginationRequest PaginationRequest { get; set; }

        public GetPagedUploadersQuery(PaginationRequest paginationRequest)
        {
            PaginationRequest = paginationRequest;
        }
    }
}
