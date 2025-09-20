using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using MediatR;

namespace ImageManagement.Api.Application.Queries.Images
{
    public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, Image?>
    {
        private readonly IImageRepository _imageRepository;

        public GetImageByIdQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Image?> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
        {
            return await _imageRepository.GetByIdAsync(request.Id);
        }
    }
}
