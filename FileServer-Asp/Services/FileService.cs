using System;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using FileServer_Asp.Configurations;
using FileServer_Asp.HelperServices;
using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;
using FileServer_Asp.Services.Abstract;
using Microsoft.Extensions.Options;
using Serilog;

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

        public async Task<bool> RemoveFileAsync(string fidId)
        {
            if (string.IsNullOrWhiteSpace(fidId))
            {
                throw new ArgumentNullException(nameof(fidId), "No Fid Id is provided.");
            }

            string assignedUrl = $"{_config.HelperBaseUrl + "8080"}/{fidId}";

            HttpResponseMessage response = await _httpClient.DeleteAsync(assignedUrl);

            if (response.IsSuccessStatusCode)
            {
                Log.Information("File removed successfully - Assign Fid: " + fidId);

                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<AssignModel> UploadFileAsync(FileModel fileToUpload)
        {
            if(fileToUpload == null)
            {
                throw new ApplicationException("No file is given.");
            }

            AssignModel assign = await _helper.GenerateFidAsync(_httpClient, _config.MasterUrl);

            if(assign == null)
            {
                throw new ArgumentNullException();
            }
            
            string assignedUrl = _config.HelperBaseUrl + fileToUpload.Port + "/" + assign.Fid;

            using var content = new MultipartFormDataContent();
            
            using var stream = fileToUpload.File.OpenReadStream();

            var fileContent = new StreamContent(stream);
            
            content.Add(fileContent, "myfile", fileToUpload.File.FileName);
            content.Add(new StringContent(fileToUpload.File.Length.ToString()), "fileLength");

            var response = await _httpClient.PostAsync(assignedUrl, content);

            response.EnsureSuccessStatusCode();

            Log.Information("File uploaded successfully - Assign Fid: " + assign.Fid);

            return assign;
        }
    }
}
