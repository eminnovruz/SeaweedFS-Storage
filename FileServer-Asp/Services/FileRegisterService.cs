using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;
using FileServer_Asp.Services.Abstract;

namespace FileServer_Asp.Services;

public class FileRegisterService : IFileRegisterService
{
    public Task<bool> RegisterFile(FileModel fileToUpload)
    {


        throw new NotImplementedException();
    }

    public Task<AssignJsonModel> ViewFileViaSecretName(string secretName)
    {
        throw new NotImplementedException();
    }
}
