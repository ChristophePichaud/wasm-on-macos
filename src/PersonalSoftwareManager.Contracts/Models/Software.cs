namespace PersonalSoftwareManager.Contracts.Models;

public class Software
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Screenshot> Screenshots { get; set; } = new List<Screenshot>();
    public ICollection<Url> Urls { get; set; } = new List<Url>();
}
