using Ardalis.Result;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetPagedDocumentsQueryHandler : IRequestHandler<GetPagedDocumentsQuery, Result<PaginationResponse<Document>>>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetPagedDocumentsQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Result<PaginationResponse<Document>>> Handle(GetPagedDocumentsQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PaginationRequest.PageIndex;
            int pageSize = request.PaginationRequest.PageSize;
            bool isDescending = request.PaginationRequest.IsDescending;
            string keyword = request.PaginationRequest.KeyWord;
            DateOnly? fromDate = request.PaginationRequest.FromDate;
            DateOnly? toDate = request.PaginationRequest.ToDate;

            try
            {
                var (item, totalCount) = await _documentRepository.GetPagedAsync(pageIndex, pageSize, isDescending, keyword, fromDate, toDate);
                return Result.Success(new PaginationResponse<Document>(pageIndex, pageSize, totalCount, item));

            }
            catch (Exception ex)
            {
                return Result.Error("Error when procesing with database: " + ex);
            }
        }
    }
}
