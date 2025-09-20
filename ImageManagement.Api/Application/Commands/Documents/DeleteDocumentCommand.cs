using Ardalis.Result;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Documents
{
    public class DeleteDocumentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteDocumentCommand(Guid id)
        {
            Id = id;
        }
    }
}
