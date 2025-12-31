# Personal Software Manager - Getting Started Guide

## Quick Start

This guide will help you get the Personal Software Manager application up and running.

## Prerequisites

Before you begin, ensure you have:
- .NET 10 SDK installed (version 10.0.101 or later)
- PostgreSQL database server installed and running
- A code editor (Visual Studio 2022, VS Code, or Rider recommended)

## Setup Steps

### 1. Database Configuration

#### Install PostgreSQL (if not already installed)

**macOS:**
```bash
brew install postgresql@15
brew services start postgresql@15
```

**Ubuntu/Linux:**
```bash
sudo apt-get update
sudo apt-get install postgresql postgresql-contrib
sudo systemctl start postgresql
```

**Windows:**
Download and install from [PostgreSQL Downloads](https://www.postgresql.org/download/windows/)

#### Create Database

```bash
# Connect to PostgreSQL
psql -U postgres

# Create the database
CREATE DATABASE PersonalSoftwareManager;

# Exit
\q
```

### 2. Configure Connection String

Update the connection string in `src/PersonalSoftwareManager.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=PersonalSoftwareManager;Username=postgres;Password=your_password"
  }
}
```

**Note:** For security, consider using user secrets for development:
```bash
cd src/PersonalSoftwareManager.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=PersonalSoftwareManager;Username=postgres;Password=your_password"
```

### 3. Build the Solution

```bash
dotnet build
```

## Running the Application

### Option 1: Run API and Blazor WASM Separately

**Terminal 1 - Start the API:**
```bash
cd src/PersonalSoftwareManager.Api
dotnet run
```

The API will start at `https://localhost:5001` (or as specified in launchSettings.json)

**Terminal 2 - Start the Blazor WASM App:**
```bash
cd src/PersonalSoftwareManager.Wasm
dotnet run
```

The Blazor app will start and open in your default browser.

### Option 2: Run from Visual Studio

1. Open `PersonalSoftwareManager.sln` in Visual Studio
2. Set multiple startup projects:
   - Right-click solution → Properties → Startup Project
   - Select "Multiple startup projects"
   - Set both `PersonalSoftwareManager.Api` and `PersonalSoftwareManager.Wasm` to "Start"
3. Press F5 to run

### Option 3: Test with Console Application

The console application can be used to test the database and API without running the full web application:

```bash
cd src/PersonalSoftwareManager.Console
dotnet run
```

**Note:** Make sure the API is running before testing API calls from the console app.

## Using the Application

### Blazor WASM Frontend

Once the Blazor app is running, you can:

1. **Navigate to Software Page** (`/software`)
   - View all personal software projects
   - See versions, descriptions, and creation dates

2. **Navigate to Projects Page** (`/projects`)
   - View tracked open-source projects
   - See GitHub stats (stars, forks, language)
   - Click links to view projects on GitHub

### API Testing

You can test the API directly using:

#### Using curl

```bash
# Get all software
curl -X GET https://localhost:5001/api/software

# Get all open source projects
curl -X GET https://localhost:5001/api/opensourceprojects

# Create new software
curl -X POST https://localhost:5001/api/software \
  -H "Content-Type: application/json" \
  -d '{
    "name": "My Software",
    "description": "Test software",
    "version": "1.0.0"
  }'
```

#### Using the included .http file

If using Visual Studio 2022 or VS Code with REST Client extension:
1. Open `src/PersonalSoftwareManager.Api/PersonalSoftwareManager.Api.http`
2. Click "Send Request" above any request

### Console Application Features

The console application provides automated tests for:

**Entity Framework Tests:**
- Database connection verification
- Creating test data
- Querying data
- Relationship verification

**API Tests:**
- HTTP connectivity
- GET endpoint testing
- Data retrieval validation

## Database Initialization

The application uses Entity Framework Core's `EnsureCreated()` method, which automatically creates the database schema on first run.

### Alternative: Use Migrations

If you prefer using EF Core migrations:

```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Create initial migration
cd src/PersonalSoftwareManager.Data
dotnet ef migrations add InitialCreate --startup-project ../PersonalSoftwareManager.Api

# Apply migration
dotnet ef database update --startup-project ../PersonalSoftwareManager.Api
```

## Project URLs

Default URLs (can be changed in launchSettings.json):

- **API**: https://localhost:5001
- **API Swagger/OpenAPI**: https://localhost:5001/openapi/v1.json (in development mode)
- **Blazor WASM**: https://localhost:5001 (or separate port if configured)

## Troubleshooting

### Database Connection Issues

**Error: "connection refused" or "could not connect"**
- Verify PostgreSQL is running: `pg_isready`
- Check connection string credentials
- Ensure database exists: `psql -U postgres -l`

### Port Already in Use

If you get "port already in use" errors:

1. Change ports in `Properties/launchSettings.json` for each project
2. Update CORS policy in `PersonalSoftwareManager.Api/Program.cs`
3. Update HttpClient base address in `PersonalSoftwareManager.Wasm/Program.cs`

### CORS Errors

If the Blazor app can't connect to the API:
- Verify the API's CORS policy includes your Blazor app's URL
- Check the HttpClient base address in Blazor's Program.cs
- Ensure both projects are running

### Build Errors

**Version conflicts:**
- Clean the solution: `dotnet clean`
- Delete bin and obj folders
- Rebuild: `dotnet build`

## Next Steps

1. **Add Data**: Use the API or console app to add software and project entries
2. **Customize**: Modify the models to fit your specific needs
3. **Extend**: Add more pages, features, or API endpoints
4. **Deploy**: Consider deploying to Azure, AWS, or other cloud platforms

## Development Tips

- Use `dotnet watch` for hot reload during development:
  ```bash
  cd src/PersonalSoftwareManager.Api
  dotnet watch run
  ```

- Enable detailed logging in appsettings.Development.json:
  ```json
  {
    "Logging": {
      "LogLevel": {
        "Default": "Debug",
        "Microsoft.EntityFrameworkCore": "Information"
      }
    }
  }
  ```

- Use browser developer tools (F12) to debug the Blazor app

## Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Blazor Documentation](https://docs.microsoft.com/aspnet/core/blazor)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
