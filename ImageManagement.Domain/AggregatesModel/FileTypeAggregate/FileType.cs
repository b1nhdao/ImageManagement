using ImageManagement.Domain.FileBase;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.FileTypeAggregate
{
    public class FileType : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public FileType(string name)
        {
            Name = name;
        }

        public void AddFileToFoldeType(BaseFile file)
        {

        }
    }
}
