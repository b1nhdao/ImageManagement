using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteMultipleDocumentsCommand : IRequest<bool>
    {
        public IEnumerable<Guid> Ids { get; set; }

        public DeleteMultipleDocumentsCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }
    }
}
