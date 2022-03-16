using UtilityProject.Application;

namespace BookStore.Web.Common
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void Delete(string filePath)
        {
            if (filePath != null)
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\images\\ProductPictures\\{filePath}";
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        public string Upload(IFormFile file, string path, string? existFile = null)
        {
            if (file == null)
                return "";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}\\images\\ProductPictures\\{path}";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(directoryPath);


            //Check If Image For Product Exists Remove it
            if (existFile != null)
            {
                var existFilePath = $"{_webHostEnvironment.WebRootPath}\\images\\ProductPictures\\{existFile}";
                if (File.Exists(existFilePath))
                    File.Delete(existFilePath);
            }
           

            var g = Guid.NewGuid();
            var fileName = $"{DateTime.Now.ToFileTime()}-{g}{Path.GetExtension(file.FileName)}";

            var filePath = $"{directoryPath}//{fileName}";
            using (var output = File.Create(filePath))
                file.CopyTo(output);


            return $"{path}/{fileName}";
        }
    }
}
