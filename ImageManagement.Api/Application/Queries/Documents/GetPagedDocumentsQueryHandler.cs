using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Documents
{
    public class GetPagedDocumentsQueryHandler : IRequestHandler<GetPagedDocumentsQuery, PaginationResponse<Document>>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetPagedDocumentsQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<PaginationResponse<Document>> Handle(GetPagedDocumentsQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PaginationRequest.PageIndex;
            int pageSize = request.PaginationRequest.PageSize;
            bool isDescending = request.PaginationRequest.IsDescending;
            string keyword = request.PaginationRequest.KeyWord;
            DateOnly? fromDate = request.PaginationRequest.FromDate;
            DateOnly? toDate = request.PaginationRequest.ToDate;

            var (item, totalCount) = await _documentRepository.GetPagedAsync(pageIndex, pageSize, isDescending, keyword, fromDate, toDate);

            return new PaginationResponse<Document>(pageIndex, pageSize, totalCount, item);
        }
    }
}
