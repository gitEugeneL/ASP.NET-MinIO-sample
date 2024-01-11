namespace Application.Files;

public sealed record FileResponse(
    MemoryStream MemoryStream,
    string ContentType,
    string FileName
);
