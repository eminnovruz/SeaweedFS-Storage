using FileServer_Asp.Models;

namespace FileServer_Asp.Services.Abstract;

public interface IFileService
{
    Task<bool> UploadFileAsync(FileModel fileToUpload);
    Task<string> ReadFileAsync(string fidId);
}
