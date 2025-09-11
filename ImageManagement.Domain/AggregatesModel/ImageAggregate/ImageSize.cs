using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class ImageSize : ValueObject
    {
        public int Height { get; private set; } 
        public int Width { get; private set; }

        public ImageSize(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public ImageSize()
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Height;
            yield return Width;
        }
    }
}