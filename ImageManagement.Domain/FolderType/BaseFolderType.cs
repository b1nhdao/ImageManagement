namespace ImageManagement.Domain.FolderType
{
    public abstract class BaseFolderType
    {
        public string Name { get; private set; }
        public string TargetFolder { get; private set; }
        public string Description { get; private set; }
        public abstract string CreateLocation();

        protected BaseFolderType(string name, string description)
        {
            Name = name;
            TargetFolder = CreateLocation();
            Description = description;
        }
    }
}
