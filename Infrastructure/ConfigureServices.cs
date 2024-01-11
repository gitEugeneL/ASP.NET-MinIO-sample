using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
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
        services
            .AddScoped<IFileDataRepository, FileDataDataRepository>()
            .AddScoped<IFileManager, FileManager>();
        
        /*** Database connection ***/
        services.AddDbContext<AppDbContext>(option =>
            option.UseNpgsql(config.GetConnectionString("PSQL")));
        
        /*** MinIO connection ***/
        services.AddMinio(options =>
        {
            var acceskey = "BhNFKYG03CZ7xIPGOYxu";
            var secretKey = "bsuI4s1VxWbTL0iu5TC8jhdZNcMXx7VBdxhj37Rt";
            
            options.WithEndpoint(config.GetSection("MinIOConnection:Endpoint").Value);
            options.WithCredentials(
                acceskey,
                secretKey
            );
            options.WithSSL(false);
            options.Build();
        });
        
        return services;
    }
}
