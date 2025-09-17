namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class ChiFolder : BaseFolder
    {
        public const string FOLDER_TYPE = "chi";

        public ChiFolder() : base(FOLDER_TYPE)
        {
        }

        public override string CreateLocation()
        {
            return $"{DateTime.Now.Year}-{DateTime.Now.Month:D2}-{FOLDER_TYPE}";
        }
    }
}
