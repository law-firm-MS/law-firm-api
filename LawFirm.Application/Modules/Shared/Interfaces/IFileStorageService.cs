using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LawFirm.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string subfolder);
        Task<byte[]> GetFileAsync(string filePath);
    }
}
