using FileServer_Asp.CustomExceptions;
using FileServer_Asp.Data;
using FileServer_Asp.Entities;
using FileServer_Asp.Models;
using FileServer_Asp.Services.Abstract;
using MongoDB.Driver;

namespace FileServer_Asp.Services;

/// <summary>
/// Service class for handling file registration and retrieval.
/// </summary>
public class FileRegisterService : IFileRegisterService
{
    private readonly MongoDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileRegisterService"/> class with the specified MongoDB context.
    /// </summary>
    /// <param name="context">The MongoDB context.</param>
    public FileRegisterService(MongoDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Registers a new file in the database.
    /// </summary>
    /// <param name="fileToUpload">The file model containing the file information to upload.</param>
    /// <param name="fid">The file identifier.</param>
    /// <param name="publicUrl">The public URL of the file.</param>
    /// <exception cref="ExistingAssignException">Thrown when an assign with the given secret name already exists.</exception>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RegisterFile(FileModel fileToUpload, string fid, string publicUrl)
    {
        if (fileToUpload == null)
        {
            throw new ArgumentNullException(nameof(fileToUpload));
        }

        FilterDefinition<AssignEntity> filter = Builders<AssignEntity>.Filter.Eq(a => a.SecretName, fileToUpload.SecretName);

        AssignEntity existingEntity = await _context.Assigns.Find(filter).FirstOrDefaultAsync();

        if (existingEntity != null)
        {
            throw new ExistingAssignException();
        }

        AssignEntity newEntity = new AssignEntity
        {
            Id = Guid.NewGuid().ToString(),
            SecretName = fileToUpload.SecretName,
            Fid = fid,
            PublicUrl = publicUrl
        };

        await _context.Assigns.InsertOneAsync(newEntity);
    }

    public async Task<bool> RemoveFile(string fid)
    {
        FilterDefinition<AssignEntity> filter = Builders<AssignEntity>.Filter.Eq(a => a.Fid, fid);

        DeleteResult result = await _context.Assigns.DeleteOneAsync(filter);

        return result.IsAcknowledged;
    }

    /// <summary>
    /// Retrieves an assign entity based on the secret name.
    /// </summary>
    /// <param name="secretName">The secret name of the file.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the assign entity, or null if not found.</returns>
    public async Task<AssignEntity> ViewFileViaSecretName(string secretName)
    {
        if (string.IsNullOrEmpty(secretName))
        {
            throw new ArgumentException("Secret name cannot be null or empty", nameof(secretName));
        }

        FilterDefinition<AssignEntity> filter = Builders<AssignEntity>.Filter.Eq(f => f.SecretName, secretName);

        AssignEntity assign = await _context.Assigns.Find(filter).FirstOrDefaultAsync();

        return assign;
    }
}
