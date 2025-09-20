using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteDocumentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteDocumentCommand(Guid id)
        {
            Id = id;
        }
    }
}
