using Microsoft.AspNetCore.Http;

namespace UtilityProject.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path , string? existFile = null);
        void Delete(string path);
    }
}