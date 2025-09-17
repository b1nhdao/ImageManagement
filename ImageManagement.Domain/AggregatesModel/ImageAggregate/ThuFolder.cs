namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class ThuFolder : BaseFolder
    {
        public const string FOLDER_TYPE = "thu";

        public ThuFolder() : base(FOLDER_TYPE)
        {
        }


        public override string CreateLocation()
        {
            return $"{DateTime.Now.Year}-{DateTime.Now.Month:D2}-{FOLDER_TYPE}";
        }
    }
}
