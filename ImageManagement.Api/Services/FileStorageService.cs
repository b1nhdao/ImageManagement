using System.Text.RegularExpressions;
using ImageManagement.Api.Services.Interfaces;
using ImageManagement.Domain.FolderType;

namespace ImageManagement.Api.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public FileStorageService(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        public string CreateUploadDirectory(BaseFolder folder, string fileCategory)
        {
            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadDirectory = Path.Combine(webRoot, "uploads", fileCategory, folder.TargetFolder);
            Directory.CreateDirectory(uploadDirectory);
            return uploadDirectory;
        }

        public string GenerateFileName(string originalFileName, string extension)
        {
            var datePrefix = DateTime.Now.ToString("ddMMyyHHmmss");
            var baseName = GetSafeFileName(originalFileName);
            return $"{datePrefix}_{baseName}{extension}";
        }

        public async Task SaveFileAsync(IFormFile file, string fullPath, CancellationToken cancellationToken = default)
        {
            await using var fileStream = new FileStream(fullPath, FileMode.CreateNew);
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        public async Task DeleteFileAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));

            if (File.Exists(physicalPath))
            {
                await Task.Run(() => File.Delete(physicalPath), cancellationToken);
            }
        }

        public async Task DeleteMultipleFilesAsync(IEnumerable<string> relativePaths, CancellationToken cancellationToken = default)
        {
            foreach (var path in relativePaths)
            {
                await DeleteFileAsync(path, cancellationToken);
            }
        }

        public long ValidateAndGetFileSizeKb(IFormFile file, string configKey)
        {
            var fileSize = file.Length / 1024; // converting bytes to kb
            var maximumUploadSize = _configuration.GetValue<int>(configKey);

            if (fileSize > maximumUploadSize)
            {
                throw new InvalidOperationException($"File upload can't be higher than {maximumUploadSize} KB");
            }

            return fileSize;
        }

        private static string GetSafeFileName(string originalFileName)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            if (string.IsNullOrWhiteSpace(nameWithoutExtension))
            {
                nameWithoutExtension = "file";
            }
            var safeName = Regex.Replace(nameWithoutExtension, @"[^\w\-_]", "");
            return string.IsNullOrEmpty(safeName) ? "file" : safeName;
        }

    }
}
