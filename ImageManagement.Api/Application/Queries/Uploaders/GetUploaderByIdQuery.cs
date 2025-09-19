using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Uploaders
{
    public class GetUploaderByIdQuery : IRequest<Uploader?>
    {
        public int Id { get; set; }

        public GetUploaderByIdQuery(int id)
        {
            Id = id;
        }
    }
}
