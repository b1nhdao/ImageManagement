namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public abstract class BaseFolder
    {
        public string Name { get; private set; }
        public string TargetFolder { get; private set; }
        public string Description { get; private set; }
        public abstract string CreateLocation();

        protected BaseFolder(string name, string description)
        {
            Name = name;
            TargetFolder = CreateLocation();
            Description = description;
        }
    }
}
