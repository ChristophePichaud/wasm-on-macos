namespace PersonalSoftwareManager.Contracts.Models;

public class Screenshot
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Foreign keys
    public int? SoftwareId { get; set; }
    public int? OpenSourceProjectId { get; set; }
    
    // Navigation properties
    public Software? Software { get; set; }
    public OpenSourceProject? OpenSourceProject { get; set; }
}
