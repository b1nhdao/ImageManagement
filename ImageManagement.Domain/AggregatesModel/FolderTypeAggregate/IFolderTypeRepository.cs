using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Domain.AggregatesModel.FolderTypeAggregate
{
    public interface IFolderTypeRepository : IRepository
    {
        IEnumerable<FolderType> GetFileTypes();
        FolderType AddFolderType(FolderType folderType);
        FolderType GetFolderTypeById(Guid id);
        FolderType DeleteFolderType(Guid id);
    }
}
