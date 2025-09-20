namespace ImageManagement.Domain.AggregatesModel.FolderTypeAggregate
{
    public class ThuFolderType : FolderType
    {
        public const string FOLDER_TYPE = "thu";

        public ThuFolderType(string name, string description) : base(name, description)
        {
        }

        public override string CreateLocation()
        {
            var now = DateTime.Now;
            return $"{now.Year}/{now.Month:D2}/{FOLDER_TYPE}";
        }
    }
}
