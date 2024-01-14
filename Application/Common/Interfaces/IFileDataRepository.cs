using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IFileDataRepository
{
    Task<FileData> CreateFileData(FileData fileData, CancellationToken cancellationToken);

    Task<FileData?> FindFileDataByFileName(string fileName, CancellationToken cancellationToken);
    
    Task<FileData?> FindFileDataByFileNameAndBucketName(
        string bucketName, string fileName, CancellationToken cancellationToken);

    Task<List<string>> FindNamesByBucket(string bucketName, CancellationToken cancellationToken);
    
    Task DeleteFileData(FileData fileData, CancellationToken cancellationToken);
}
