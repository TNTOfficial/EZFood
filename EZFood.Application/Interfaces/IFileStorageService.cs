using Microsoft.AspNetCore.Http;
namespace EZFood.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string subDirectory, string fileName);
    void DeleteFile(string filePath);
}
