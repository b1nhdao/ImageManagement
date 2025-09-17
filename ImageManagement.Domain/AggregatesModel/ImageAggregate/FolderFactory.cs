namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public static class FolderFactory
    {
        public static BaseFolder CreateFolder(ImageType imageType)
        {
            return imageType switch
            {
                ImageType.Chi => new ChiFolder(),
                ImageType.Thu => new ThuFolder(),
                _ => new OtherFolder()
            };
        }
    }
}
