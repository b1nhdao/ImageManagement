using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class DeleteMultipleImagesCommand : IRequest<bool>
    {
        public IEnumerable<Guid> Ids { get; set; }
        public DeleteMultipleImagesCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }
    }
}
