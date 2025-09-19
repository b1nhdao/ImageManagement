namespace ImageManagement.Domain.FolderType
{
    public class ChiFolder : BaseFolder
    {
        public const string FOLDER_TYPE = "chi";

        public ChiFolder(string name, string description) : base(name, description)
        {
        }

        public override string CreateLocation()
        {
            var now = DateTime.Now;
            return $"{now.Year}/{now.Month:D2}/{FOLDER_TYPE}";
        }
    }
}
