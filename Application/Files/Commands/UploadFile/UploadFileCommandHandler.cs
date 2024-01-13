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
        
        var fileName = request.FileName.ToLower();
        var fileData = await fileDataRepository.FindFileDataByFileName(fileName, cancellationToken);
        if (fileData is not null)
        {
            var ext = Path.GetExtension(fileData.FileName);
            var name = Path.ChangeExtension(fileData.FileName, null);
            var copy = 1;
            do fileName = $"{name}({copy++}){ext}";
            while (await fileDataRepository.FindFileDataByFileName(fileName, cancellationToken) is not null);
        }
        
        if (!await fileManager.BucketExists(bucketName))
            await fileManager.CreateBucket(bucketName);
        
        await fileManager
            .UploadFile(bucketName, fileName, request.FileType, request.FileLength, request.Stream);

        var createdFileData = await fileDataRepository.CreateFileData(
            new FileData
            {
                BucketName = bucketName,
                FileName = fileName,
                FileType = request.FileType,
                FileSize = (ulong) request.FileLength
            },
            cancellationToken
        );

        return createdFileData.FileName;
    }
}
