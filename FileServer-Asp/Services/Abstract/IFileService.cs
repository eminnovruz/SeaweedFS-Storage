namespace FileServer_Asp.Services.Abstract;

public interface IFileService
{
    Task<bool> UploadFileAsync(IFormFile fileToUpload);
    Task<string> ReadFileAsync(string fidId);
}
