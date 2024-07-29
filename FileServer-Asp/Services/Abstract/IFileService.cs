namespace FileServer_Asp.Services.Abstract;

public interface IFileService
{
    Task<bool> WriteFileAsync(string filePath, byte[] content);
    Task<byte[]> ReadFileAsync(string filePath);
}
