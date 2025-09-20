using ImageManagement.Domain.AggregatesModel.FileTypeAggregate;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace ImageManagement.Domain.FileBase
{
    public class BaseFile : Entity
    {
        public string Url { get; private set; }
        public string Name { get; private set; }
        public long Size { get; private set; } // in KB
        public DateTime UploadedTime { get; private set; }
        public int UploaderId { get; private set; }
        public Guid FolderTypeId { get; private set; }
        [JsonIgnore]
        public Uploader Uploader { get; }
        [JsonIgnore]
        public FileType FileType { get; private set; }

        public BaseFile(string url, string name, long size, DateTime uploadedTime, int uploaderId, Guid fileTypeId)
        {
            Url = url;
            Name = name;
            Size = size;
            UploadedTime = uploadedTime;
            UploaderId = uploaderId;
            FolderTypeId = fileTypeId;
        }

        public BaseFile()
        {
        }
    }
}
