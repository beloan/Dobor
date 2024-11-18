using Web.Abstractions.ServiceAbstractions;

namespace Web.Services
{
    public class FileUploadService : IFileUploadService
    {
        IHostEnvironment _env;

        public FileUploadService(IHostEnvironment hostEnvironment)
        {
            _env = hostEnvironment;
        }

        public async Task<string?> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine("wwwroot", "images"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return file.FileName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
