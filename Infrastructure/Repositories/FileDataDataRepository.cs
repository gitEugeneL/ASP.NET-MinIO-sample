using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class FileDataDataRepository(AppDbContext context) : IFileDataRepository
{
    public async Task<FileData> CreateFileData(FileData fileData, CancellationToken cancellationToken)
    {
        await context.FileData
            .AddAsync(fileData, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return fileData;
    }

    public async Task<FileData?> FindFileDataByFileName(string fileName, CancellationToken cancellationToken)
    {
        return await context.FileData
            .FirstOrDefaultAsync(fd => fd.FileName == fileName.ToLower(), cancellationToken);
    }

    public async Task<FileData?> 
        FindFileDataByFileNameAndBucketName(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        return await context.FileData
            .FirstOrDefaultAsync(
                fd => fd.FileName == fileName.ToLower() 
                      && fd.BucketName == bucketName.ToLower(), 
                cancellationToken);
    }

    public async Task<List<string>> FindNamesByBucket(string bucketName, CancellationToken cancellationToken)
    {
        return await context.FileData
            .Where(fd => fd.BucketName == bucketName.ToLower())
            .Select(fd => fd.FileName)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteFileData(FileData fileData, CancellationToken cancellationToken)
    {
        context.FileData.Remove(fileData);
        await context.SaveChangesAsync(cancellationToken);
    }
}
