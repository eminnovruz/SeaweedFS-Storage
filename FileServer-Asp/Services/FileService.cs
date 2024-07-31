using System;
using System.Net.Http;
using System.Threading.Tasks;
using FileServer_Asp.Configurations;
using FileServer_Asp.HelperServices;
using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;
using FileServer_Asp.Services.Abstract;
using Microsoft.Extensions.Options;

namespace FileServer_Asp.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly SeaweedConfiguration _config;
        private readonly SeaweedFsHelper _helper;

        public FileService(HttpClient httpClient, IOptions<SeaweedConfiguration> seaweedConfig)
        {
            _config = seaweedConfig.Value;

            _httpClient = new HttpClient();

            _helper = new SeaweedFsHelper();
        }

        public Task<string> ReadFileAsync(string fidId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadFileAsync(FileModel fileToUpload)
        {
            if(fileToUpload == null)
            {
                throw new ApplicationException("No file is given.");
            }

            AssignModel assign = await _helper.GenerateFidAsync(_httpClient, _config.MasterUrl);
            string assignedUrl = "http://" + assign.PublicUrl + "/" + assign.Fid;

            using var content = new MultipartFormDataContent();
            using var stream = fileToUpload.File.OpenReadStream();

            var fileContent = new StreamContent(stream);
            content.Add(fileContent, "myfile", fileToUpload.File.FileName);
            content.Add(new StringContent(fileToUpload.File.Length.ToString()), "fileLength");

            var response = await _httpClient.PostAsync(assignedUrl, content);

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
