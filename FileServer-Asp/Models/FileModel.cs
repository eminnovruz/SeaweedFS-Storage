namespace FileServer_Asp.Models;

public class FileModel
{
    public string SecretName { get; set; }
    public IFormFile File { get; set; }
    public string Port { get; set; }
}
