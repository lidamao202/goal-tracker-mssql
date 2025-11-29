namespace goal_api.Models;

public class Goal
{
    public int Id { get; set; }
    public int TeamMemberId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
