using MediatR;

namespace Application.Files.Queries.GetFiles;

public sealed record GetFilesQuery(
    string BucketName
) : IRequest<List<string>>;
