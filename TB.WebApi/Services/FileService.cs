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
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
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
    }
}
