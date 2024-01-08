using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IFileManager, FileManager>();
        
        /*** Database connection ***/
        services.AddDbContext<AppDbContext>(option =>
            option.UseNpgsql(config.GetSection("PSQL").Value));
        
        /*** MinIO connection ***/
        services.AddMinio(options =>
        {
            options.WithEndpoint(config.GetSection("MinIOConnection:Endpoint").Value);
            options.WithCredentials(
                config.GetSection("MinIOConnection:AccessKey").Value,
                config.GetSection("MinIOConnection:SecretKey").Value
            );
            options.Build();
        });
        
        return services;
    }
}
