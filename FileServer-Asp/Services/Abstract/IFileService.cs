using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;

namespace FileServer_Asp.Services.Abstract;

public interface IFileService
{
    Task<AssignJsonModel> UploadFileAsync(FileModel fileToUpload);
    Task<string> ReadFileAsync(string fidId);
    Task<string> ReadFileViaSecretNameAsync(string secret);
    Task<bool> RemoveFileAsync(string fidId);
}
