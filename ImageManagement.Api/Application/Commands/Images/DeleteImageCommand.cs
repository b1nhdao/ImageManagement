using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class DeleteImageCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteImageCommand(Guid id)
        {
            Id = id;
        }
    }
}
