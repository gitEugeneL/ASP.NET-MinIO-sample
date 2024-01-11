using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class FileConfiguration : IEntityTypeConfiguration<FileData>
{
    public void Configure(EntityTypeBuilder<FileData> builder)
    {
        builder.HasIndex(fd => fd.FileName)
            .IsUnique();
        
        builder.Property(fd => fd.BucketName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(fd => fd.FileName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(fd => fd.FileType)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(fd => fd.FileSize)
            .IsRequired();
        
        builder.Property(fd => fd.Created)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(fd => fd.LastModified)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
