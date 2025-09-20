using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.FileTypeAggregate
{
    public interface IFileTypeRepository : IRepository
    {
        FileType AddFolderType(FileType folderType);
        FileType GetFolderTypeById(Guid id);
        FileType DeleteFolderType(Guid id);
    }
}
