using ImageManagement.Domain.FileBase;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.DocumentAggregate
{
    public class Document : BaseFile, IAggregateRoot
    {
        public Document(string url, string name, long size, DateTime uploadedTime, int uploaderId, Guid folderTypeId) : base(url, name, size, uploadedTime, uploaderId, folderTypeId)
        {
            Id = Guid.NewGuid();
        }

        public Document() : base()
        {

        }
    }
}
