using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;

namespace FileServer_Asp.Services.Abstract;

public interface IFileService
{
    Task<AssignModel> UploadFileAsync(FileModel fileToUpload);
    Task<string> ReadFileAsync(string fidId);
    Task<bool> RemoveFileAsync(string fidId);
}
