using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IFileDataRepository
{
    Task<FileData> CreateFileData(FileData fileData, CancellationToken cancellationToken);
}
