# DOTNET-MinIO-sample
Sample project to integration Minio with ASP.NET Core 8 

https://min.io/docs/minio/linux/developers/dotnet/API.html

## Main technologies

* [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)
* [Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core)
* [MediatR](https://github.com/jbogard/MediatR)
* [MinIO](https://min.io/docs/minio/linux/developers/dotnet/API.html)
* [PostgreSQL](https://www.postgresql.org/)
* [Docker](https://www.docker.com)

## List of containers

* **database** - postgreSQL database container.

* **minIO** - file storage container.

* **app** - container for all application layers.

## How to run the server

1. Build and start Docker images based on the configuration defined in the docker-compose.yml.

        make up     // docker-compose up --build

2. Stop and remove containers.

        make down   // docker-compose down

## API documentation

1. Swagger documentation

        http://localhost:8080/swagger/index.html
