using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.FolderTypeAggregate
{
    public abstract class FolderType : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string TargetFolder { get; private set; }
        public string Description { get; private set; }
        public abstract string CreateLocation();

        protected FolderType(string name, string description)
        {
            Name = name;
            TargetFolder = CreateLocation();
            Description = description;
        }
    }
}
