using goal_api.Models;

namespace goal_api.DTOs;

public class TeamMemberDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Mood Mood { get; set; }
    public List<GoalDto> Goals { get; set; } = new();
}
