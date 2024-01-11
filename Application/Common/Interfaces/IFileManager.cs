namespace Application.Common.Interfaces;

public interface IFileManager
{
    Task<bool> BucketExists(string name);
    
    Task CreateBucket(string name);

    Task UploadFile(string bucketName, string fileName, string fileType, long fileLength, Stream fileStream);
}
