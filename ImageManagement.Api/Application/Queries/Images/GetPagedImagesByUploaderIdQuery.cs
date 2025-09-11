using ImageManagement.Api.Models.Pagination;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Images
{
    public class GetPagedImagesByUploaderIdQuery : IRequest<PaginationResponse<Image>>
    {
        public Guid UploaderId { get; set; }
        public PaginationRequest PaginationRequest { get; set; }

        public GetPagedImagesByUploaderIdQuery(Guid uploaderId, PaginationRequest request)
        {
            UploaderId = uploaderId;
            PaginationRequest = request;
        }
    }
}
