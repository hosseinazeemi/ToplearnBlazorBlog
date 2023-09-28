using TB.Shared.Dto.Global;

namespace TB.WebApi.Services
{
    public interface IFileService
    {
        bool Delete(string fileName, string folderName);
        string Save(FileDto file, string folderName);
    }
    public class FileService:IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContext;
        public FileService(IWebHostEnvironment env, IHttpContextAccessor httpContext)
        {
            _env = env;
            _httpContext = httpContext;
        }

        public string Save(FileDto file ,string folderName)
        {
            string fileName = $"{Guid.NewGuid()}.{file.Extension}";
            string directory = Path.Combine(_env.WebRootPath , folderName);
            string path = Path.Combine(directory , fileName);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllBytesAsync(path , file.Bytes).GetAwaiter();

            return fileName;
        }

        public bool Delete(string fileName , string folderName)
        {
            string find = Path.GetFileName(fileName);
            string address = Path.Combine(_env.WebRootPath , folderName , find);

            if (File.Exists(address))
                File.Delete(address);

            return true;
        }

        public async Task<string> Save(IFormFile file , string folderName)
        {
            string fileName = $"{Guid.NewGuid() + Path.GetExtension(file.FileName)}";
            string directory = Path.Combine(_env.WebRootPath , folderName);
            string path = Path.Combine(directory , fileName);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (file.Length > 0)
            {
                using (Stream fileStream = new FileStream(path , FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            // https :// google.com / images / a.png
            string fullUrl = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/{folderName}/{fileName}";
           
            return fullUrl;
        }
    }
}
