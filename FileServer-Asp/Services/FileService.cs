using FileServer_Asp.Configurations.SeaweedFs;
using FileServer_Asp.HelperServices;
using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;
using FileServer_Asp.Services.Abstract;
using Microsoft.Extensions.Options;
using Serilog;

namespace FileServer_Asp.Services;

/// <summary>
/// Service for managing files in SeaweedFS.
/// </summary>
public class FileService : IFileService
{
    private readonly HttpClient _httpClient;
    private readonly SeaweedConfiguration _config;
    private readonly SeaweedFsHelper _helper;
    private readonly IFileRegisterService _registerService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client for sending requests.</param>
    /// <param name="seaweedConfig">The SeaweedFS configuration.</param>
    /// <param name="registerService">The file register service.</param>
    public FileService(HttpClient httpClient, IOptions<SeaweedConfiguration> seaweedConfig, IFileRegisterService registerService)
    {
        _config = seaweedConfig?.Value ?? throw new ArgumentNullException(nameof(seaweedConfig));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _helper = new SeaweedFsHelper();
        _registerService = registerService ?? throw new ArgumentNullException(nameof(registerService));
    }

    /// <summary>
    /// Reads a file's content asynchronously by its file identifier.
    /// </summary>
    /// <param name="fidId">The file identifier.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<string> ReadFileAsync(string fidId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reads the file's public URL via the secret name asynchronously.
    /// </summary>
    /// <param name="secret">The secret name of the file.</param>
    /// <returns>A task representing the asynchronous operation, with the public URL of the file.</returns>
    public async Task<string> ReadFileViaSecretNameAsync(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new ArgumentException("Secret name cannot be null or empty", nameof(secret));
        }

        var assign = await _registerService.ViewFileViaSecretName(secret);
        return assign?.PublicUrl + "/" + assign.Fid;
    }

    /// <summary>
    /// Removes a file asynchronously by its file identifier.
    /// </summary>
    /// <param name="fidId">The file identifier.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean value indicating success or failure.</returns>
    public async Task<bool> RemoveFileAsync(string fidId)
    {
        if (string.IsNullOrWhiteSpace(fidId))
        {
            throw new ArgumentNullException(nameof(fidId), "No Fid Id is provided.");
        }

        var assignedUrl = $"{_config.HelperBaseUrl}8080/{fidId}";

        var response = await _httpClient.DeleteAsync(assignedUrl);

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

    /// <summary>
    /// Uploads a file asynchronously.
    /// </summary>
    /// <param name="fileToUpload">The file model containing the file information to upload.</param>
    /// <returns>A task representing the asynchronous operation, with the assigned JSON model.</returns>
    /// <exception cref="ApplicationException">Thrown when the file to upload is null.</exception>
    public async Task<AssignJsonModel> UploadFileAsync(FileModel fileToUpload)
    {
        if (fileToUpload == null)
        {
            throw new ApplicationException("No file is given.");
        }

        var assign = await _helper.GenerateFidAsync(_httpClient, _config.MasterUrl);

        if (assign == null)
        {
            throw new ArgumentNullException(nameof(assign), "Failed to generate file identifier.");
        }

        var assignedUrl = $"{_config.HelperBaseUrl}{fileToUpload.Port}/{assign.Fid}";

        using var content = new MultipartFormDataContent();
        using var stream = fileToUpload.File.OpenReadStream();
        var fileContent = new StreamContent(stream);

        content.Add(fileContent, "myfile", fileToUpload.File.FileName);
        content.Add(new StringContent(fileToUpload.File.Length.ToString()), "fileLength");

        var response = await _httpClient.PostAsync(assignedUrl, content);

        response.EnsureSuccessStatusCode();

        await _registerService.RegisterFile(fileToUpload, assign.Fid, assign.PublicUrl);

        Log.Information("File uploaded successfully - Assign Fid: " + assign.Fid);

        return assign;
    }
}
