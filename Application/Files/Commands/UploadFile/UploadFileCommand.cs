using MediatR;

namespace Application.Files.Commands.UploadFile;

public sealed record UploadFileCommand(
    Stream Stream,
    string FileType,
    long FileLength
) : IRequest<string>;
