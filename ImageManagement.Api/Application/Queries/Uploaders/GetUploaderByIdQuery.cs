using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Uploaders
{
    public class GetUploaderByIdQuery : IRequest<Uploader?>
    {
        public Guid Id { get; set; }

        public GetUploaderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
