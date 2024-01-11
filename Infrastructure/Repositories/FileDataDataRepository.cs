using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

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
}
