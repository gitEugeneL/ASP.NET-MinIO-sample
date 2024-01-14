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
            options.WithEndpoint(config.GetSection("MinIOConnection:Endpoint").Value);
            options.WithCredentials(
                config.GetSection("MinIOConnection:AccessKey").Value,
                config.GetSection("MinIOConnection:SecretKey").Value
            );
            options.WithSSL(false);
            options.Build();
        });
        
        /*** Upload develop database ***/
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            AppDbContextInitializer
                .Init(services.BuildServiceProvider().GetRequiredService<AppDbContext>());
        
        return services;
    }
}
