using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalSoftwareManager.Contracts.Models;
using PersonalSoftwareManager.Data.Context;
using System.Net.Http.Json;

Console.WriteLine("=== Personal Software Manager Console ===");
Console.WriteLine();

// Setup dependency injection
var services = new ServiceCollection();
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=PersonalSoftwareManager;Username=postgres;Password=admin"));

var serviceProvider = services.BuildServiceProvider();

// Test EF Core functionality
await TestEntityFramework(serviceProvider);

// Test API calls
await TestApiCalls();

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static async Task TestEntityFramework(ServiceProvider serviceProvider)
{
    Console.WriteLine("--- Testing Entity Framework ---");
    
    try
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Ensure database is created
        Console.WriteLine("Ensuring database exists...");
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("✓ Database ready");
        
        // Test adding a software entry
        Console.WriteLine("Adding test software entry...");
        var software = new Software
        {
            Name = "Test Software",
            Description = "A test software entry",
            Version = "1.0.0",
            CreatedAt = DateTime.UtcNow
        };
        
        context.Software.Add(software);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Software added with ID: {software.Id}");
        
        // Test querying
        Console.WriteLine("Querying software entries...");
        var allSoftware = await context.Software.ToListAsync();
        Console.WriteLine($"✓ Found {allSoftware.Count} software entries");
        
        foreach (var sw in allSoftware)
        {
            Console.WriteLine($"  - {sw.Name} (v{sw.Version})");
        }
        
        // Test adding an open source project
        Console.WriteLine("Adding test open source project...");
        var project = new OpenSourceProject
        {
            Name = "Test Project",
            Description = "A test open source project",
            GitHubUrl = "https://github.com/test/project",
            Owner = "test",
            Language = "C#",
            Stars = 100,
            Forks = 10,
            CreatedAt = DateTime.UtcNow
        };
        
        context.OpenSourceProjects.Add(project);
        await context.SaveChangesAsync();
        Console.WriteLine($"✓ Project added with ID: {project.Id}");
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error: {ex.Message}");
        Console.WriteLine("Note: Make sure PostgreSQL is running and accessible");
    }
}

static async Task TestApiCalls()
{
    Console.WriteLine();
    Console.WriteLine("--- Testing API Calls ---");
    
    try
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };
        
        Console.WriteLine("Attempting to connect to API at https://localhost:5001...");
        Console.WriteLine("Note: Make sure the API is running");
        
        // Test getting software
        var software = await httpClient.GetFromJsonAsync<List<Software>>("api/software");
        Console.WriteLine($"✓ Retrieved {software?.Count ?? 0} software entries from API");
        
        // Test getting open source projects
        var projects = await httpClient.GetFromJsonAsync<List<OpenSourceProject>>("api/opensourceprojects");
        Console.WriteLine($"✓ Retrieved {projects?.Count ?? 0} open source projects from API");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"✗ API Error: {ex.Message}");
        Console.WriteLine("Note: Make sure the API is running on https://localhost:5001");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error: {ex.Message}");
    }
}
