using Application.Common.Interfaces;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Files.Commands.DownloadFile;

public class DownloadFileCommandHandler(
    IFileDataRepository fileDataRepository,
    IFileManager fileManager
) : IRequestHandler<DownloadFileCommand, FileResponse>
{
    public async Task<FileResponse> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
    {
        var fileData = await fileDataRepository.FindFileDataByFileNameAndBucketName(
                           request.BucketName,
                           request.FileName,
                           cancellationToken)
                       ?? throw new NotFoundException(nameof(FileData), $"{request.FileName} in {request.BucketName}");

        var stream = await fileManager.DownloadFile(request.BucketName, request.FileName);
        return new FileResponse(stream, fileData.FileType, fileData.FileName);
    }
}
