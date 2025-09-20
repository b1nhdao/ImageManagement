namespace ImageManagement.Domain.FolderType
{
    public class ChiFolderType : BaseFolderType
    {
        public const string FOLDER_TYPE = "chi";

        public ChiFolderType(string name, string description) : base(name, description)
        {
        }

        public override string CreateLocation()
        {
            var now = DateTime.Now;
            return $"{now.Year}/{now.Month:D2}/{FOLDER_TYPE}";
        }
    }
}
