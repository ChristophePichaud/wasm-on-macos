# Personal Software Manager

A .NET 10 application for managing personal software projects and tracking open-source repositories from GitHub.

## Features

- 📦 **Software Management**: Track your personal software projects with versions, descriptions, and metadata
- 🌟 **Open Source Tracking**: Monitor your favorite GitHub repositories with stats (stars, forks, language)
- 💬 **Comments**: Add notes and comments to software and projects
- 📸 **Screenshots**: Store and manage screenshots for your projects
- 🔗 **URLs**: Keep track of related links and documentation
- 🗄️ **PostgreSQL**: Robust database backend with Entity Framework Core
- 🚀 **Modern Stack**: Built with .NET 10, Blazor WebAssembly, and ASP.NET Core Web API

## Project Structure

```
PersonalSoftwareManager/
├── src/
│   ├── PersonalSoftwareManager.Wasm/      # Blazor WebAssembly frontend
│   ├── PersonalSoftwareManager.Api/       # ASP.NET Core Web API
│   ├── PersonalSoftwareManager.Contracts/ # Shared models and contracts
│   ├── PersonalSoftwareManager.Data/      # Entity Framework data layer
│   └── PersonalSoftwareManager.Console/   # Console application for testing
├── Docs/                                   # Documentation
│   ├── ProjectCreation.md                 # Project creation guide
│   └── ProjectSpecifications.md           # Detailed specifications
└── PersonalSoftwareManager.sln            # Solution file
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (version 10.0.101 or later)
- [PostgreSQL](https://www.postgresql.org/download/) database
- Optional: Visual Studio 2022, Visual Studio Code, or JetBrains Rider

## Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/ChristophePichaud/wasm-on-macos.git
cd wasm-on-macos
```

### 2. Configure Database

Update the connection string in `src/PersonalSoftwareManager.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=PersonalSoftwareManager;Username=postgres;Password=your_password"
  }
}
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the API

```bash
cd src/PersonalSoftwareManager.Api
dotnet run
```

The API will be available at `https://localhost:5001`

### 5. Run the Blazor WASM App

Open a new terminal:

```bash
cd src/PersonalSoftwareManager.Wasm
dotnet run
```

### 6. Test with Console App (Optional)

```bash
cd src/PersonalSoftwareManager.Console
dotnet run
```

## Documentation

- [Project Creation Guide](Docs/ProjectCreation.md) - Step-by-step guide to recreate the project
- [Project Specifications](Docs/ProjectSpecifications.md) - Detailed technical specifications

## Technology Stack

- **Frontend**: Blazor WebAssembly (.NET 10)
- **Backend**: ASP.NET Core Web API (.NET 10)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 10.0
- **Package Management**: NuGet

## API Endpoints

### Software
- `GET /api/software` - List all software
- `GET /api/software/{id}` - Get software by ID
- `POST /api/software` - Create new software
- `PUT /api/software/{id}` - Update software
- `DELETE /api/software/{id}` - Delete software

### Open Source Projects
- `GET /api/opensourceprojects` - List all projects
- `GET /api/opensourceprojects/{id}` - Get project by ID
- `POST /api/opensourceprojects` - Create new project
- `PUT /api/opensourceprojects/{id}` - Update project
- `DELETE /api/opensourceprojects/{id}` - Delete project

## Data Model

The application manages:
- **Software**: Personal software projects with versions and descriptions
- **OpenSourceProject**: GitHub repositories with stars, forks, and language info
- **Comment**: Notes and comments for software/projects
- **Screenshot**: Image references for projects
- **Url**: Related links and documentation

See [Project Specifications](Docs/ProjectSpecifications.md) for complete data model details.

## Development

### Build

```bash
dotnet build
```

### Run Tests (Console App)

```bash
cd src/PersonalSoftwareManager.Console
dotnet run
```

### Database Migrations

```bash
cd src/PersonalSoftwareManager.Data
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is open source and available under the MIT License.

## Support

For issues, questions, or suggestions, please open an issue on GitHub.
