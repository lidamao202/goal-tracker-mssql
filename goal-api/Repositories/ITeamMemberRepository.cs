using goal_api.Models;

namespace goal_api.Repositories;

public interface ITeamMemberRepository
{
    Task<IEnumerable<TeamMember>> GetAllAsync();
    Task<TeamMember?> GetByIdAsync(int id);
    Task<TeamMember?> UpdateMoodAsync(int id, Mood mood);
}
