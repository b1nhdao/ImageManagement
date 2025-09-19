using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Images
{
    public class GetPagedImagesByUserIdQuery : IRequest<PaginationResponse<Image>>
    {
        public int UserId { get; set; }
        public PaginationRequest PaginationRequest { get; set; }

        public GetPagedImagesByUserIdQuery(int userId, PaginationRequest request)
        {
            UserId = userId;
            PaginationRequest = request;
        }
    }
}
