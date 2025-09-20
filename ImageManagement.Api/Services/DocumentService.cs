using ImageManagement.Api.Models.FileModels;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.FolderType;

namespace ImageManagement.Api.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IFileStorageService _fileStorageService;

        public DocumentService(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<IEnumerable<DocumentUploadResult>> UploadMultipleDocumentsAsync(IEnumerable<IFormFile> files, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files), "Files collection cannot be null");

            var fileList = files.ToList();
            if (fileList.Count == 0)
                return [];

            var results = new List<DocumentUploadResult>();
            var folder = FolderFactory.CreateFolder(folderTypeKey);

            var uploadedPath = new List<string>();

            foreach (var file in fileList)
            {
                try
                {
                    var documentUploadResult = await ConstructDocumentUploadResult(file, folder, cancellationToken);
                    results.Add(documentUploadResult);
                    uploadedPath.Add(documentUploadResult.RelativeUrl);
                }
                catch (Exception ex)
                {
                    // delete all uploaded files if exception occurs
                    if (uploadedPath.Any())
                    {
                        await _fileStorageService.DeleteMultipleFilesAsync(uploadedPath, cancellationToken);
                    }

                    throw new InvalidOperationException($"Failed to upload file '{file?.FileName}': {ex.Message}", ex);
                }
            }
            return results;
        }

        public async Task<DocumentUploadResult> UploadDocumentAsync(IFormFile file, int uploaderId, string folderTypeKey, CancellationToken cancellationToken = default)
        {
            var folder = FolderFactory.CreateFolder(folderTypeKey);
            return await ConstructDocumentUploadResult(file, folder, cancellationToken);
        }

        public async Task DeleteDocumentAsync(string path, CancellationToken cancellationToken = default)
        {
            await _fileStorageService.DeleteFileAsync(path, cancellationToken);
        }

        public async Task DeleteMultipleDocumentsAsync(IEnumerable<string> paths, CancellationToken cancellationToken = default)
        {
            await _fileStorageService.DeleteMultipleFilesAsync(paths, cancellationToken);
        }

        private async Task<DocumentUploadResult> ConstructDocumentUploadResult(IFormFile file, BaseFolder folder, CancellationToken cancellationToken)
        {
            var validFile = FileValidationService.ValidateFile(file);
            var fileExtension = FileValidationService.GetValidatedDocumentExtension(validFile);

            var documentSize = _fileStorageService.ValidateAndGetFileSizeKb(file, "DocumentConfiguration:MaximumUploadPerFile");

            var uploadDirectory = _fileStorageService.CreateUploadDirectory(folder, "documents");
            var generatedFileName = _fileStorageService.GenerateFileName(validFile.FileName, fileExtension);
            var fullPath = Path.Combine(uploadDirectory, generatedFileName);

            await _fileStorageService.SaveFileAsync(validFile, fullPath, cancellationToken);

            var relativeUrl = $"/uploads/documents/{folder.TargetFolder}/{generatedFileName}";

            return new DocumentUploadResult(
                relativeUrl,
                generatedFileName,
                documentSize,
                validFile.FileName,
                fileExtension
            );
        }
    }
}
