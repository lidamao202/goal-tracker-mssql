using goal_api.DTOs;
using goal_api.Models;
using goal_api.Repositories;

namespace goal_api.Services;

public class GoalTrackerService : IGoalTrackerService
{
    private readonly ITeamMemberRepository _memberRepository;
    private readonly IGoalRepository _goalRepository;

    public GoalTrackerService(ITeamMemberRepository memberRepository, IGoalRepository goalRepository)
    {
        _memberRepository = memberRepository;
        _goalRepository = goalRepository;
    }

    public async Task<(List<TeamMemberDto> Members, DashboardStatsDto Stats)> GetDashboardAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        var goals = await _goalRepository.GetAllAsync();
        var goalsList = goals.ToList();

        var memberDtos = members.Select(m => new TeamMemberDto
        {
            Id = m.Id,
            Name = m.Name,
            Mood = m.Mood,
            Goals = goalsList.Where(g => g.TeamMemberId == m.Id)
                .Select(g => new GoalDto
                {
                    Id = g.Id,
                    TeamMemberId = g.TeamMemberId,
                    Description = g.Description,
                    IsCompleted = g.IsCompleted
                }).ToList()
        }).ToList();

        var stats = CalculateStats(memberDtos, goalsList);

        return (memberDtos, stats);
    }

    public async Task<IEnumerable<TeamMemberDto>> GetMembersAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        return members.Select(m => new TeamMemberDto
        {
            Id = m.Id,
            Name = m.Name,
            Mood = m.Mood,
            Goals = new List<GoalDto>()
        });
    }

    public async Task<GoalDto?> CreateGoalAsync(int teamMemberId, string description)
    {
        var member = await _memberRepository.GetByIdAsync(teamMemberId);
        if (member == null) return null;

        var goal = await _goalRepository.CreateAsync(teamMemberId, description);
        return new GoalDto
        {
            Id = goal.Id,
            TeamMemberId = goal.TeamMemberId,
            Description = goal.Description,
            IsCompleted = goal.IsCompleted
        };
    }

    public async Task<GoalDto?> ToggleGoalAsync(int id)
    {
        var goal = await _goalRepository.ToggleCompletedAsync(id);
        if (goal == null) return null;

        return new GoalDto
        {
            Id = goal.Id,
            TeamMemberId = goal.TeamMemberId,
            Description = goal.Description,
            IsCompleted = goal.IsCompleted
        };
    }

    public async Task<bool> DeleteGoalAsync(int id)
    {
        return await _goalRepository.DeleteAsync(id);
    }

    public async Task<TeamMemberDto?> UpdateMoodAsync(int memberId, Mood mood)
    {
        var member = await _memberRepository.UpdateMoodAsync(memberId, mood);
        if (member == null) return null;

        var goals = await _goalRepository.GetByMemberIdAsync(memberId);
        return new TeamMemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Mood = member.Mood,
            Goals = goals.Select(g => new GoalDto
            {
                Id = g.Id,
                TeamMemberId = g.TeamMemberId,
                Description = g.Description,
                IsCompleted = g.IsCompleted
            }).ToList()
        };
    }

    private static DashboardStatsDto CalculateStats(List<TeamMemberDto> members, List<Goal> goals)
    {
        var totalGoals = goals.Count;
        var completedGoals = goals.Count(g => g.IsCompleted);
        var completionPercentage = totalGoals > 0 ? (int)Math.Round((double)completedGoals / totalGoals * 100) : 0;

        var moodDistribution = members
            .GroupBy(m => m.Mood)
            .Select(g => new MoodCountDto { Mood = g.Key, Count = g.Count() })
            .OrderBy(m => (int)m.Mood)
            .ToList();

        var dominantMood = moodDistribution.MaxBy(m => m.Count)?.Mood;

        return new DashboardStatsDto
        {
            TotalGoals = totalGoals,
            CompletedGoals = completedGoals,
            CompletionPercentage = completionPercentage,
            MoodDistribution = moodDistribution,
            DominantMood = dominantMood
        };
    }
}
