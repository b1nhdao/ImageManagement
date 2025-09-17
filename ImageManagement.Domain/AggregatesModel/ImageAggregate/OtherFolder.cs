namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class OtherFolder : BaseFolder
    {
        private const string FOLDER_TYPE = "other";

        public OtherFolder() : base(FOLDER_TYPE)
        {
        }

        public override string CreateLocation()
        {
            return $"{FOLDER_TYPE}";
        }
    }
}
