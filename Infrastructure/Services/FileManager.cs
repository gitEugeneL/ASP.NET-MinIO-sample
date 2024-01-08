using Application.Common.Interfaces;
using Minio;

namespace Infrastructure.Services;

internal class FileManager(MinioClient client) : IFileManager
{
    public Task CreateBucket(string name)
    {
        throw new NotImplementedException();
    }
}
