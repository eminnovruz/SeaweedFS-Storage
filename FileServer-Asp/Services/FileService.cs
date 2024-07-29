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
        private readonly SeaweedConfiguration _config;
        private readonly HttpClient _httpClient;

        public FileService(IOptions<SeaweedConfiguration> config)
        {
            _config = config.Value;
            _httpClient = new HttpClient();
        }

        public async Task<byte[]> ReadFileAsync(string filePath)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_config.VolumeUrl}/{filePath}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                throw new IOException("Error reading file from SeaweedFS", ex);
            }
        }

        public async Task<bool> WriteFileAsync(string filePath, byte[] content)
        {
            try
            {
                var url = $"{_config.MasterUrl}/{filePath}";
                using var contentStream = new ByteArrayContent(content);
                var formData = new MultipartFormDataContent
                {
                    { contentStream, "file", filePath }
                };
                var response = await _httpClient.PostAsync(url, formData);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                throw new IOException("Error writing file to SeaweedFS", ex);
            }
        }
    }
}
