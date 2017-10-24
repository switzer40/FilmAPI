### EF Migrations Commands

// Run from Web Project Folder
dotnet ef migrations add Initial -p ..\FilmAPI.Infrastructure\FilmAPI.Infrastructure.csproj -s .\FilmAPI.csproj -o Data/Migrations
dotnet ef database update
