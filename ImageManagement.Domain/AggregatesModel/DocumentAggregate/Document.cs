using ImageManagement.Domain.AggregatesModel.FileBaseAggregate;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.DocumentAggregate
{
    public class Document : BaseFile, IAggregateRoot
    {
        public Document(string url, string name, long size, DateTime uploadedTime, int uploaderId) : base(url, name, size, uploadedTime, uploaderId)
        {
            Id = Guid.NewGuid();
        }

        public Document() : base()
        {

        }
    }
}
