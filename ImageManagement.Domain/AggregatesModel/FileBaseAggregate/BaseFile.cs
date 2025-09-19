using System.Text.Json.Serialization;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.FileBaseAggregate
{
    public class BaseFile : Entity
    {
        public string Url { get; private set; }
        public string Name { get; private set; }
        public long Size { get; private set; } // in KB
        public DateTime UploadedTime { get; private set; }
        public int UploaderId { get; private set; }
        [JsonIgnore]
        public Uploader Uploader { get; }

        public BaseFile(string url, string name, long size, DateTime uploadedTime, int uploaderId)
        {
            Url = url;
            Name = name;
            Size = size;
            UploadedTime = uploadedTime;
            UploaderId = uploaderId;
        }

        public BaseFile()
        {
        }
    }
}
