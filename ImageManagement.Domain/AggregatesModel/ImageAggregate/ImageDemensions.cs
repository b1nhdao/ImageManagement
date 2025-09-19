using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class ImageDemensions : ValueObject
    {
        public int Height { get; private set; } 
        public int Width { get; private set; }

        public ImageDemensions(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public ImageDemensions()
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Height;
            yield return Width;
        }
    }
}