using BookStore.Interfaces;

namespace BookStore.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Task<bool> DeleteFileAsync(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return Task.FromResult(false);
            }

            var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public async Task<string?> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file is null || file.Length == 0) {
                return null;
            }

            //extension 
            List<string> validExtensions = new List<string>() { ".jpg", ".png", ".gif"};
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if(!validExtensions.Contains(extension))
            {
                var allowedExts = string.Join(',', validExtensions);
                throw new ArgumentException($"Неверное расширение. Разрешены: {allowedExts}.");
            }

            //file size
            if (file.Length > 20 * 1024 * 1024 )
            {
                throw new ArgumentException("Файл слишком большой. Максимум - 20МБ.");
            }

            //name changing
            var safeFileName = Guid.NewGuid().ToString() + extension;

            //путь к папке в wwwroot
            var uploadDir = Path.Combine(_environment.WebRootPath, folderName);
            Directory.CreateDirectory(uploadDir); 

            var fullPath = Path.Combine(uploadDir, safeFileName);

            //сохранение
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderName, safeFileName).Replace("\\", "/");
        }
    }
}
