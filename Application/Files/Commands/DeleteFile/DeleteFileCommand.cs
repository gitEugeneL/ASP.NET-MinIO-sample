using MediatR;

namespace Application.Files.Commands.DeleteFile;

public sealed record DeleteFileCommand(
    string BucketName,
    string FileName
) : IRequest<Unit>;