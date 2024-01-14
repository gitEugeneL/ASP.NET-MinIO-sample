using Application.Common.Interfaces;
using MediatR;

namespace Application.Files.Queries.GetFiles;

public class GetFilesQueryHandler(
    IFileDataRepository fileDataRepository
) : IRequestHandler<GetFilesQuery, List<string>>
{
    public async Task<List<string>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
       return await fileDataRepository.FindNamesByBucket(request.BucketName, cancellationToken);
    }
}
