using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;

namespace FileServer_Asp.Services.Abstract;

public interface IFileRegisterService
{
    public Task<bool> RegisterFile(FileModel fileToUpload);
    public Task<AssignModel> ViewFileViaSecretName(string secretName);
}
