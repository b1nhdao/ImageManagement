namespace ImageManagement.Domain.AggregatesModel.FolderTypeAggregate
{
    public class OtherFolderType : FolderType
    {
        private const string FOLDER_TYPE = "other";

        public OtherFolderType(string name, string description) : base(name, description)
        {
        }

        public override string CreateLocation()
        {
            var now = DateTime.Now;
            return $"{now.Year}/{now.Month:D2}/{FOLDER_TYPE}";
        }
    }
}
