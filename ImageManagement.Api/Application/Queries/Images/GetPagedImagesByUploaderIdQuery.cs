using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Images
{
    public class GetPagedImagesByUploaderIdQuery : IRequest<PaginationResponse<Image>>
    {
        public int UploaderId { get; set; }
        public PaginationRequest PaginationRequest { get; set; }

        public GetPagedImagesByUploaderIdQuery(int uploaderId, PaginationRequest request)
        {
            UploaderId = uploaderId;
            PaginationRequest = request;
        }
    }
}
