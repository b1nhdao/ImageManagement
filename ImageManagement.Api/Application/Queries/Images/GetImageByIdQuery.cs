using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Images
{
    public class GetImageByIdQuery : IRequest<Image?>
    {
        public Guid Id { get; set; }

        public GetImageByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
