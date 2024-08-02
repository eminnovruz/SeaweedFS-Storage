namespace FileServer_Asp.Models;

/// <summary>
/// Model representing the file to be uploaded.
/// </summary>
public class FileModel
{
    /// <summary>
    /// Gets or sets the secret name of the file.
    /// </summary>
    public string SecretName { get; set; }

    /// <summary>
    /// Gets or sets the file to be uploaded.
    /// </summary>
    public IFormFile File { get; set; }
}
