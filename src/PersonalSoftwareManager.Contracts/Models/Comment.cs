namespace PersonalSoftwareManager.Contracts.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Foreign keys
    public int? SoftwareId { get; set; }
    public int? OpenSourceProjectId { get; set; }
    
    // Navigation properties
    public Software? Software { get; set; }
    public OpenSourceProject? OpenSourceProject { get; set; }
}
