namespace ImageManagement.Api.Services
{
    public static class FileValidationService
    {
        public static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png"];
        public static readonly string[] AllowedDocumentExtensions = [".pdf", ".doc", ".docx", ".txt", ".xls", ".xlsx"];

        public static IFormFile ValidateFile(IFormFile file)
        {
            return file ?? throw new ArgumentNullException(nameof(file), "File cannot be null");
        }

        public static string GetValidatedExtension(IFormFile file, string[] allowedExtensions)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                throw new NotSupportedException($"Unsupported file extension: {extension}. " +
                    $"Allowed extensions: {string.Join(", ", allowedExtensions)}");
            }
            return extension;
        }

        public static string GetValidatedImageExtension(IFormFile file)
        {
            return GetValidatedExtension(file, AllowedImageExtensions);
        }

        public static string GetValidatedDocumentExtension(IFormFile file)
        {
            return GetValidatedExtension(file, AllowedDocumentExtensions);
        }
    }
}
