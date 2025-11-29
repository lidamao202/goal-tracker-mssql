namespace goal_api.DTOs;

public class DashboardResponse
{
    public List<TeamMemberDto> Members { get; set; } = new();
    public DashboardStatsDto Stats { get; set; } = new();
}
