namespace PersonalSoftwareManager.Contracts.Models;

public class Url
{
    public int Id { get; set; }
    public string Link { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Foreign keys
    public int? SoftwareId { get; set; }
    public int? OpenSourceProjectId { get; set; }
    
    // Navigation properties
    public Software? Software { get; set; }
    public OpenSourceProject? OpenSourceProject { get; set; }
}
