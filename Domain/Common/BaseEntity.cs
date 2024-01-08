namespace Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime Created { get; init; }
    public DateTime LastModified { get; set; }
}
