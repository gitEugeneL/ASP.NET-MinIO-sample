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

    public async Task<MemoryStream> DownloadFile(string bucketName, string fileName)
    {
        var stream = new MemoryStream();
        var tsc = new TaskCompletionSource<bool>();

        var getObjectArgs = new GetObjectArgs()
            .WithBucket(bucketName.ToLower())
            .WithObject(fileName.ToLower())
            .WithCallbackStream(cs =>
            {
                cs.CopyTo(stream);
                tsc.SetResult(true);
            });

        await client.GetObjectAsync(getObjectArgs);
        await tsc.Task;
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    public async Task DeleteFile(string bucketName, string fileName)
    {
        await client.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(bucketName.ToLower())
            .WithObject(fileName.ToLower())
        );
    }
}
