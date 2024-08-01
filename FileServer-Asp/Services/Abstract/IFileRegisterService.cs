using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;

namespace FileServer_Asp.Services.Abstract;

public interface IFileRegisterService
{
    public Task RegisterFile(FileModel fileToUpload, string fid);
    public Task<AssignJsonModel> ViewFileViaSecretName(string secretName);
}
