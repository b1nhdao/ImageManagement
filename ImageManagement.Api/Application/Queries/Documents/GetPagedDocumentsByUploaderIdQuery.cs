using Ardalis.Result;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetPagedDocumentsByUploaderIdQuery : IRequest<Result<PaginationResponse<Document>>>
    {
        public int UploaderId { get; set; }
        public PaginationRequest PaginationRequest { get; set;}

        public GetPagedDocumentsByUploaderIdQuery(int uploaderId, PaginationRequest paginationRequest)
        {
            UploaderId = uploaderId;
            PaginationRequest = paginationRequest;
        }
    }
}
