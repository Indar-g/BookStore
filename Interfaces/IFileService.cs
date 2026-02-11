namespace BookStore.Interfaces
{
    public interface IFileService
    {
        Task<string?> SaveFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string relativePath);
    }
}
