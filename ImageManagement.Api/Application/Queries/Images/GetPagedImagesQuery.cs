using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Images
{
    public class GetPagedImagesQuery : IRequest<PaginationResponse<Image>>
    {
        public PaginationRequest PaginationRequest { get; set; }

        public GetPagedImagesQuery(PaginationRequest paginationRequest)
        {
            PaginationRequest = paginationRequest;
        }
    }
}
