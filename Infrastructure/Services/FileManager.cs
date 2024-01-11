using Application.Common.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Services;

internal class FileManager(IMinioClient client) : IFileManager
{
    public async Task<bool> BucketExists(string name)
    {
        return await client.BucketExistsAsync(
                new BucketExistsArgs()
                    .WithBucket(name)
            );
    }
    
    public async Task CreateBucket(string name)
    {
        await client.MakeBucketAsync(
                new MakeBucketArgs()
                    .WithBucket(name)
            );
    }

    public async Task UploadFile(string bucketName, string fileName, string fileType, long fileLength, Stream fileStream)
    {
        await client.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileLength)
                .WithContentType(fileType)
        );
    }
}
