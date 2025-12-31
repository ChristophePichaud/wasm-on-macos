# Personal Software Manager - Project Creation Guide

## Overview
This guide walks through the creation of the Personal Software Manager solution, a .NET 10 application for managing personal software projects and tracking open-source repositories.

## Prerequisites
- .NET 10 SDK (version 10.0.101 or later)
- PostgreSQL database
- Visual Studio 2022, Visual Studio Code, or Rider (optional)

## Solution Structure

The solution consists of 5 projects:

1. **PersonalSoftwareManager.Wasm** - Blazor WebAssembly frontend
2. **PersonalSoftwareManager.Api** - ASP.NET Core Web API backend
3. **PersonalSoftwareManager.Contracts** - Shared models and contracts
4. **PersonalSoftwareManager.Data** - Entity Framework Core data layer
5. **PersonalSoftwareManager.Console** - Console application for testing

## Creating the Solution from Scratch

### 1. Create the Solution

```bash
dotnet new sln -n PersonalSoftwareManager
```

### 2. Create the Projects

```bash
# Create src directory
mkdir src
cd src

# Create Blazor WASM project
dotnet new blazorwasm -n PersonalSoftwareManager.Wasm

# Create Web API project
dotnet new webapi -n PersonalSoftwareManager.Api

# Create class libraries
dotnet new classlib -n PersonalSoftwareManager.Contracts
dotnet new classlib -n PersonalSoftwareManager.Data

# Create console application
dotnet new console -n PersonalSoftwareManager.Console

cd ..
```

### 3. Add Projects to Solution

```bash
dotnet sln add src/PersonalSoftwareManager.Wasm/PersonalSoftwareManager.Wasm.csproj
dotnet sln add src/PersonalSoftwareManager.Api/PersonalSoftwareManager.Api.csproj
dotnet sln add src/PersonalSoftwareManager.Contracts/PersonalSoftwareManager.Contracts.csproj
dotnet sln add src/PersonalSoftwareManager.Data/PersonalSoftwareManager.Data.csproj
dotnet sln add src/PersonalSoftwareManager.Console/PersonalSoftwareManager.Console.csproj
```

### 4. Add Project References

```bash
# Data layer needs Contracts
cd src/PersonalSoftwareManager.Data
dotnet add reference ../PersonalSoftwareManager.Contracts/PersonalSoftwareManager.Contracts.csproj

# API needs Contracts and Data
cd ../PersonalSoftwareManager.Api
dotnet add reference ../PersonalSoftwareManager.Contracts/PersonalSoftwareManager.Contracts.csproj
dotnet add reference ../PersonalSoftwareManager.Data/PersonalSoftwareManager.Data.csproj

# Blazor WASM needs Contracts
cd ../PersonalSoftwareManager.Wasm
dotnet add reference ../PersonalSoftwareManager.Contracts/PersonalSoftwareManager.Contracts.csproj

# Console app needs Contracts and Data
cd ../PersonalSoftwareManager.Console
dotnet add reference ../PersonalSoftwareManager.Contracts/PersonalSoftwareManager.Contracts.csproj
dotnet add reference ../PersonalSoftwareManager.Data/PersonalSoftwareManager.Data.csproj
```

### 5. Add NuGet Packages

```bash
# Add Entity Framework packages to Data layer
cd ../PersonalSoftwareManager.Data
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design

# Add packages to Console app
cd ../PersonalSoftwareManager.Console
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
```

### 6. Database Setup

Ensure PostgreSQL is installed and running. The default connection string is:
```
Host=localhost;Database=PersonalSoftwareManager;Username=postgres;Password=postgres
```

Update the connection string in `appsettings.json` as needed for your environment.

## Building the Solution

```bash
# Build entire solution
dotnet build

# Build specific project
dotnet build src/PersonalSoftwareManager.Api
```

## Running the Projects

### Run the API
```bash
cd src/PersonalSoftwareManager.Api
dotnet run
```
The API will be available at `https://localhost:5001`

### Run the Blazor WASM App
```bash
cd src/PersonalSoftwareManager.Wasm
dotnet run
```
The app will be available at `https://localhost:5001` (or as specified)

### Run the Console App
```bash
cd src/PersonalSoftwareManager.Console
dotnet run
```

## Database Migrations (Optional)

If you want to use EF Core migrations instead of `EnsureCreated()`:

```bash
cd src/PersonalSoftwareManager.Data
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Next Steps

1. Configure your PostgreSQL database connection
2. Run the API project
3. Run the Blazor WASM project
4. Test with the Console application
5. Customize the models and add more features

## Troubleshooting

- **Database Connection Issues**: Verify PostgreSQL is running and the connection string is correct
- **Port Conflicts**: Change ports in `launchSettings.json` if needed
- **CORS Errors**: Ensure the API CORS policy includes your Blazor app's URL
