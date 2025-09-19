using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class Demensions : ValueObject
    {
        public int Height { get; private set; } 
        public int Width { get; private set; }

        public Demensions(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public Demensions()
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Height;
            yield return Width;
        }
    }
}