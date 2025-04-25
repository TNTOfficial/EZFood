using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using EZFood.Shared.Exceptions;
using EZFood.Application.Interfaces;
using System.Drawing;

namespace MLM.Application.Services;

public class FileStorageService:IFileStorageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;
    private readonly string _baseUploadPath;
    private readonly long _maxFileSize;
    private readonly string[] _allowedExtensions;

    public FileStorageService(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
        _baseUploadPath = _configuration.GetValue<string>("FileStorage:UploadPath") ?? "uploads";
        _maxFileSize = _configuration.GetValue<long>("FileStorage:MaxFileSize")!;
        _allowedExtensions = _configuration.GetSection("FileStorage:AllowedExtensions").Get<string[]>() ??
                new string[] { ".jpg", ".jpeg", ".png", ".pdf" };
    }

    public async Task<string>SaveFileAsync(IFormFile file, string subDirectory,string fileName)
    {
        ValidateFile(file);

        // creating directory if not exists
        string uploadPath = Path.Combine(_environment.ContentRootPath, _baseUploadPath, subDirectory);
        if (!Directory.Exists(uploadPath)) 
        {
            Directory.CreateDirectory(uploadPath);
        }
        // Get the extension & generate unique fileName 
        string fileExtension = Path.GetExtension(file.FileName);
        string uniqueFileName = $"{fileName}_{DateTime.UtcNow.Ticks}{fileExtension}";
        string filePath = Path.Combine(uploadPath, uniqueFileName);

        using(FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        // Return relative path
        return Path.Combine(_baseUploadPath,subDirectory,uniqueFileName);

    }

    public void DeleteFile(string filePath)
    {
        string fullPath = Path.Combine(_environment.ContentRootPath, filePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private void ValidateFile(IFormFile file) 
    {
        if(file.Length <= 0)
        {
            throw new EZFoodException("File is empty");
        }
        if (file.Length > _maxFileSize)
        {
            throw new EZFoodException($"File size exceeds the limit of {_maxFileSize / 1024 / 1024} MB");
        }

        // checking file extension 
        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        bool isValidExtension = false;
        foreach (string ext in _allowedExtensions)
        {
            if (fileExtension == ext.ToLowerInvariant())
            {
                isValidExtension = true;
                break;
            }
        }
        if (!isValidExtension)
        {
            throw new EZFoodException($"File type not allowed. Allowed types: {string.Join(", ", _allowedExtensions)}");
        }

    }
}
