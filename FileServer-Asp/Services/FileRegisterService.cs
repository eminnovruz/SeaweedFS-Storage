﻿using FileServer_Asp.Data;
using FileServer_Asp.Entities;
using FileServer_Asp.JsonModels;
using FileServer_Asp.Models;
using FileServer_Asp.Services.Abstract;
using MongoDB.Driver;

namespace FileServer_Asp.Services;

public class FileRegisterService : IFileRegisterService
{
    private readonly MongoDbContext _context;

    public FileRegisterService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task RegisterFile(FileModel fileToUpload, string fid)
    {
        await _context.Assigns.InsertOneAsync(new AssignEntity()
        {
            Id = Guid.NewGuid().ToString(),
            SecretName = fileToUpload.SecretName,
            Fid = fid
        });
    }

    public async Task<AssignEntity> ViewFileViaSecretName(string secretName)
    {
        FilterDefinition<AssignEntity> filter = Builders<AssignEntity>.Filter.Eq(f => f.SecretName, secretName);

        return await _context.Assigns.Find(filter).FirstOrDefaultAsync();
    }
}
