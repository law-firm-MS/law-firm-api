using System.IO;
using System.Threading.Tasks;
using LawFirm.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LawFirm.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _basePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "UploadedFiles"
        );

        public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
        {
            var folder = Path.Combine(_basePath, subfolder);
            Directory.CreateDirectory(folder);
            var filePath = Path.Combine(folder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }

        public async Task<byte[]> GetFileAsync(string filePath)
        {
            return await File.ReadAllBytesAsync(filePath);
        }
    }
}
