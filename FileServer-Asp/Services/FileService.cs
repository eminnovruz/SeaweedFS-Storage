using System;
using System.Net.Http;
using System.Threading.Tasks;
using FileServer_Asp.Configurations;
using FileServer_Asp.Services.Abstract;
using Microsoft.Extensions.Options;

namespace FileServer_Asp.Services
{
    public class FileService : IFileService
    {
        public Task<byte[]> ReadFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<bool> WriteFileAsync(string filePath, byte[] content)
        {
            throw new NotImplementedException();
        }
    }
}
