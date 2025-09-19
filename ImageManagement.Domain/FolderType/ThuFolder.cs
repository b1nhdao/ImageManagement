namespace ImageManagement.Domain.FolderType
{
    public class ThuFolder : BaseFolder
    {
        public const string FOLDER_TYPE = "thu";

        public ThuFolder(string name, string description) : base(name, description)
        {
        }

        public override string CreateLocation()
        {
            var now = DateTime.Now;
            return $"{now.Year}/{now.Month:D2}/{FOLDER_TYPE}";
        }
    }
}
