namespace ImageManagement.Domain.AggregatesModel.ImageAggregate
{
    public abstract class BaseFolder
    {
        public string Name { get; private set; } = string.Empty;
        public string TargetFolder { get; private set; }
        public abstract string CreateLocation();

        protected BaseFolder(string name)
        {
            Name = name;
            TargetFolder = CreateLocation();
        }
    }
}
