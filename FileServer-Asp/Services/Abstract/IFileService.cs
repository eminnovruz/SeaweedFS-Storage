using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;

namespace FileServer_Asp.Services.Abstract;

/// <summary>
/// Interface for file service operations.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Uploads a file asynchronously.
    /// </summary>
    /// <param name="fileToUpload">The file model containing the file information to upload.</param>
    /// <returns>A task representing the asynchronous operation, with the assigned JSON model.</returns>
    Task<AssignJsonModel> UploadFileAsync(FileModel fileToUpload);

    /// <summary>
    /// Reads a file's content asynchronously by its file identifier.
    /// </summary>
    /// <param name="fidId">The file identifier.</param>
    /// <returns>A task representing the asynchronous operation, with the file content.</returns>
    Task<string> ReadFileAsync(string fidId);

    /// <summary>
    /// Reads the file's public URL via the secret name asynchronously.
    /// </summary>
    /// <param name="secret">The secret name of the file.</param>
    /// <returns>A task representing the asynchronous operation, with the public URL of the file.</returns>
    Task<string> ReadFileViaSecretNameAsync(string secret);

    /// <summary>
    /// Removes a file asynchronously by its file identifier.
    /// </summary>
    /// <param name="fidId">The file identifier.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean value indicating success or failure.</returns>
    Task<bool> RemoveFileAsync(string fidId);
}
