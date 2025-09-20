using ImageManagement.Api.Models.FileModels;

namespace ImageManagement.Api.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentUploadResult> UploadDocumentAsync(IFormFile file, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default);
        Task<IEnumerable<DocumentUploadResult>> UploadMultipleDocumentsAsync(IEnumerable<IFormFile> files, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default);
        Task DeleteDocumentAsync(string path, CancellationToken cancellationToken = default);
        Task DeleteMultipleDocumentsAsync(IEnumerable<string> paths, CancellationToken cancellationToken = default);
    }
}
