using Ardalis.Result;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetDocumentByIdQuery : IRequest<Result<Document>>
    {
        public Guid Id { get; set; }

        public GetDocumentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
