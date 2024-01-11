using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Files.Commands.UploadFile;

public class UploadFileCommandHandler(IFileManager fileManager, IFileDataRepository fileDataRepository) 
    : IRequestHandler<UploadFileCommand, string>
{
    public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        const string bucketName = "example-bucket";

        if (!await fileManager.BucketExists(bucketName))
            await fileManager.CreateBucket(bucketName);
        
        var fileName = $"{Guid.NewGuid()}{request.FileType}";

        await fileManager
            .UploadFile(bucketName, fileName, request.FileType, request.FileLength, request.Stream);

        var fileData = await fileDataRepository.CreateFileData(
            new FileData
            {
                BucketName = bucketName,
                FileName = fileName,
                FileType = request.FileType,
                FileSize = (ulong) request.FileLength
            },
            cancellationToken
        );

        return fileData.FileName;
    }
}
