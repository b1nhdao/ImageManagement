using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Commands.Images
{
    public class DeleteMultipleImagesCommand : IRequest<bool>
    {
        public IEnumerable<Image> Images { get; set; }
        public DeleteMultipleImagesCommand(IEnumerable<Image> images)
        {
            Images = images;
        }
    }
}
