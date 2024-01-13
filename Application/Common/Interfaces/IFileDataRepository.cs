using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IFileDataRepository
{
    Task<FileData> CreateFileData(FileData fileData, CancellationToken cancellationToken);

    Task<FileData?> FindFileDataByFileName(string fileName, CancellationToken cancellationToken);
}
