namespace Web.Abstractions.ServiceAbstractions
{
    public interface IFileUploadService
    {
        Task<string?> UploadFile(IFormFile file);
    }
}
