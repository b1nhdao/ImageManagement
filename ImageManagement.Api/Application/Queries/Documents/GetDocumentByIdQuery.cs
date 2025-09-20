using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetDocumentByIdQuery : IRequest<Document?>
    {
        public Guid Id { get; set; }

        public GetDocumentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
