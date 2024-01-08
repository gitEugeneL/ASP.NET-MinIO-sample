namespace Application.Common.Interfaces;

public interface IFileManager
{
    Task CreateBucket(string name);
}
