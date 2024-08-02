using FileServer_Asp.Entities;
using FileServer_Asp.Models;

namespace FileServer_Asp.Services.Abstract;

/// <summary>
/// Interface for file registration and retrieval services.
/// </summary>
public interface IFileRegisterService
{
    /// <summary>
    /// Registers a new file in the database.
    /// </summary>
    /// <param name="fileToUpload">The file model containing the file information to upload.</param>
    /// <param name="fid">The file identifier.</param>
    /// <param name="publicUrl">The public URL of the file.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RegisterFile(FileModel fileToUpload, string fid, string publicUrl);

    /// <summary>
    /// Retrieves an assign entity based on the secret name.
    /// </summary>
    /// <param name="secretName">The secret name of the file.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the assign entity, or null if not found.</returns>
    Task<AssignEntity> ViewFileViaSecretName(string secretName);

    Task<bool> RemoveFile(string fid);
}
