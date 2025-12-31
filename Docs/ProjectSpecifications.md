# Personal Software Manager - Project Specifications

## Overview
Personal Software Manager is a .NET 10 application designed to help developers manage and track their personal software projects and favorite open-source repositories from GitHub.

## Architecture

### Technology Stack
- **Frontend**: Blazor WebAssembly (.NET 10)
- **Backend**: ASP.NET Core Web API (.NET 10)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 10
- **Package Manager**: NuGet

### Project Structure

```
PersonalSoftwareManager/
├── src/
│   ├── PersonalSoftwareManager.Wasm/      # Blazor WebAssembly frontend
│   ├── PersonalSoftwareManager.Api/       # ASP.NET Core Web API
│   ├── PersonalSoftwareManager.Contracts/ # Shared models and contracts
│   ├── PersonalSoftwareManager.Data/      # Entity Framework data layer
│   └── PersonalSoftwareManager.Console/   # Console application for testing
├── Docs/                                   # Documentation
└── PersonalSoftwareManager.sln            # Solution file
```

## Data Model

### Entities

#### Software
Represents personal software projects.

**Properties:**
- `Id` (int, PK) - Unique identifier
- `Name` (string, required, max 200) - Software name
- `Description` (string, max 2000) - Software description
- `Version` (string, max 50) - Current version
- `CreatedAt` (DateTime) - Creation timestamp
- `UpdatedAt` (DateTime?) - Last update timestamp

**Relationships:**
- One-to-many with Comments
- One-to-many with Screenshots
- One-to-many with Urls

#### OpenSourceProject
Represents open-source projects tracked from GitHub.

**Properties:**
- `Id` (int, PK) - Unique identifier
- `Name` (string, required, max 200) - Project name
- `Description` (string, max 2000) - Project description
- `GitHubUrl` (string, required, max 500) - GitHub repository URL
- `Owner` (string?, max 100) - Repository owner
- `Language` (string?, max 50) - Primary programming language
- `Stars` (int) - Number of GitHub stars
- `Forks` (int) - Number of forks
- `CreatedAt` (DateTime) - Creation timestamp
- `UpdatedAt` (DateTime?) - Last update timestamp

**Relationships:**
- One-to-many with Comments
- One-to-many with Screenshots
- One-to-many with Urls

#### Comment
Represents comments for software or open-source projects.

**Properties:**
- `Id` (int, PK) - Unique identifier
- `Content` (string, required, max 5000) - Comment content
- `CreatedAt` (DateTime) - Creation timestamp
- `UpdatedAt` (DateTime?) - Last update timestamp
- `SoftwareId` (int?, FK) - Foreign key to Software
- `OpenSourceProjectId` (int?, FK) - Foreign key to OpenSourceProject

**Relationships:**
- Many-to-one with Software (optional)
- Many-to-one with OpenSourceProject (optional)

#### Screenshot
Represents screenshots for software or open-source projects.

**Properties:**
- `Id` (int, PK) - Unique identifier
- `FileName` (string, required, max 200) - Screenshot file name
- `FilePath` (string, required, max 500) - Path to screenshot file
- `Description` (string?, max 500) - Screenshot description
- `CreatedAt` (DateTime) - Creation timestamp
- `SoftwareId` (int?, FK) - Foreign key to Software
- `OpenSourceProjectId` (int?, FK) - Foreign key to OpenSourceProject

**Relationships:**
- Many-to-one with Software (optional)
- Many-to-one with OpenSourceProject (optional)

#### Url
Represents URLs related to software or open-source projects.

**Properties:**
- `Id` (int, PK) - Unique identifier
- `Link` (string, required, max 1000) - URL link
- `Description` (string?, max 500) - URL description
- `CreatedAt` (DateTime) - Creation timestamp
- `SoftwareId` (int?, FK) - Foreign key to Software
- `OpenSourceProjectId` (int?, FK) - Foreign key to OpenSourceProject

**Relationships:**
- Many-to-one with Software (optional)
- Many-to-one with OpenSourceProject (optional)

## API Endpoints

### Software API (`/api/software`)

- `GET /api/software` - Get all software entries
- `GET /api/software/{id}` - Get a specific software entry
- `POST /api/software` - Create a new software entry
- `PUT /api/software/{id}` - Update a software entry
- `DELETE /api/software/{id}` - Delete a software entry

### Open Source Projects API (`/api/opensourceprojects`)

- `GET /api/opensourceprojects` - Get all open-source projects
- `GET /api/opensourceprojects/{id}` - Get a specific project
- `POST /api/opensourceprojects` - Create a new project entry
- `PUT /api/opensourceprojects/{id}` - Update a project entry
- `DELETE /api/opensourceprojects/{id}` - Delete a project entry

## Frontend Components

### Blazor WASM Pages

1. **Home** (`/`) - Landing page
2. **Software** (`/software`) - Display and manage personal software
3. **Projects** (`/projects`) - Display and manage open-source projects
4. **Counter** (`/counter`) - Demo counter page
5. **Weather** (`/weather`) - Demo weather page

## Configuration

### Database Connection

The application uses PostgreSQL with Entity Framework Core. Connection string format:
```
Host=localhost;Database=PersonalSoftwareManager;Username=postgres;Password=postgres
```

Configuration locations:
- API: `src/PersonalSoftwareManager.Api/appsettings.json`
- Console: Hardcoded in `Program.cs` (can be externalized)

### CORS Policy

The API includes a CORS policy to allow requests from the Blazor WASM frontend:
- Allowed origins: `https://localhost:5001`, `http://localhost:5000`
- Allowed methods: Any
- Allowed headers: Any

## Console Application Features

The console application provides:
1. **Entity Framework Tests**
   - Database creation/verification
   - Sample data insertion
   - Query operations
   
2. **API Call Tests**
   - HTTP GET requests to API endpoints
   - Validation of API responses

## Security Considerations

- Database credentials should be stored in user secrets or environment variables in production
- API should implement authentication and authorization for production use
- Input validation should be enhanced for production scenarios
- HTTPS is enforced by default

## Future Enhancements

Potential features for future development:
- User authentication and authorization
- File upload for screenshots
- GitHub API integration for automatic project updates
- Search and filtering capabilities
- Tagging system
- Export functionality
- Dashboard with statistics
- Real-time updates with SignalR
