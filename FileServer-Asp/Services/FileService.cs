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
        private readonly HttpClient _httpClient;
        private readonly SeaweedConfiguration _config;


        public FileService(HttpClient httpClient , IOptions<SeaweedConfiguration> seaweedConfig)
        {
            _config = seaweedConfig.Value;

            _httpClient = new HttpClient();
        }

        public Task<string> ReadFileAsync(string fidId)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IFileService.UploadFileAsync(IFormFile fileToUpload)
        {
            if(fileToUpload == null)
            {
                throw new ArgumentNullException("File is not selected.");
            }

            string url = _config.VolumeUrl;

            using var content = new MultipartFormDataContent();
            using var stream = fileToUpload.OpenReadStream();

            var fileContent = new StreamContent(stream);

            content.Add(fileContent, "myfile", fileToUpload.FileName);

            var response = await _httpClient.PostAsync(url, content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
