using goal_api.DTOs;
using goal_api.Models;

namespace goal_api.Services;

public interface IGoalTrackerService
{
    Task<(List<TeamMemberDto> Members, DashboardStatsDto Stats)> GetDashboardAsync();
    Task<IEnumerable<TeamMemberDto>> GetMembersAsync();
    Task<GoalDto?> CreateGoalAsync(int teamMemberId, string description);
    Task<GoalDto?> ToggleGoalAsync(int id);
    Task<bool> DeleteGoalAsync(int id);
    Task<TeamMemberDto?> UpdateMoodAsync(int memberId, Mood mood);
}
