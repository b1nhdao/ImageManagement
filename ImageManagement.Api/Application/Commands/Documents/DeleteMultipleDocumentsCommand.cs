using Ardalis.Result;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteMultipleDocumentsCommand : IRequest<Result>
    {
        public IEnumerable<Guid> Ids { get; set; }

        public DeleteMultipleDocumentsCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }
    }
}
