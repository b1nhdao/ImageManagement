using Ardalis.Result;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetPagedDocumentsQuery : IRequest<Result<PaginationResponse<Document>>>
    {
        public PaginationRequest PaginationRequest { get; set; }

        public GetPagedDocumentsQuery(PaginationRequest paginationRequest)
        {
            PaginationRequest = paginationRequest;
        }

    }
}
