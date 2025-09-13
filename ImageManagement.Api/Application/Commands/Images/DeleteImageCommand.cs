using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class DeleteImageCommand : IRequest<bool>
    {
        public Image Image { get; set; }

        public DeleteImageCommand(Image image)
        {
            Image = image;
        }
    }
}
