namespace goal_api.Models;

public class TeamMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Mood Mood { get; set; } = Mood.Neutral;
    public DateTime CreatedAt { get; set; }
}
