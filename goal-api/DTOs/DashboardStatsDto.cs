using goal_api.Models;

namespace goal_api.DTOs;

public class DashboardStatsDto
{
    public int TotalGoals { get; set; }
    public int CompletedGoals { get; set; }
    public int CompletionPercentage { get; set; }
    public List<MoodCountDto> MoodDistribution { get; set; } = new();
    public Mood? DominantMood { get; set; }
}
