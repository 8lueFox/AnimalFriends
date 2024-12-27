### Creating migrations:

cd.. src\AF.Infrastructure

dotnet ef migrations add migration_name -s ..\..\AF.WebApi\

dotnet ef database update -s ..\..\AF.WebApi\