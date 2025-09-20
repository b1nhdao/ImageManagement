using ImageManagement.Domain.FolderType;

namespace ImageManagement.Api.Services.Interfaces
{
    public interface IFileStorageService
    {
        string CreateUploadDirectory(BaseFolderType folder, string fileCategory);
        string GenerateFileName(string originalFileName, string extension);
        Task SaveFileAsync(IFormFile file, string fullPath, CancellationToken cancellationToken = default);
        Task DeleteFileAsync(string relativePath, CancellationToken cancellationToken = default);
        Task DeleteMultipleFilesAsync(IEnumerable<string> relativePaths, CancellationToken cancellationToken = default);
        long ValidateAndGetFileSizeKb(IFormFile file, string configKey);
    }
}
