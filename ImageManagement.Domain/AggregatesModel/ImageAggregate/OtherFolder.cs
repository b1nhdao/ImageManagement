namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public class OtherFolder : BaseFolder
    {
        private const string FOLDER_TYPE = "other";

        public OtherFolder(string name, string description) : base(name, description)
        {
        }

        public override string CreateLocation()
        {
            var now = DateTime.Now;
            return $"{now.Year}/{now.Month:D2}/{FOLDER_TYPE}";
        }
    }
}
