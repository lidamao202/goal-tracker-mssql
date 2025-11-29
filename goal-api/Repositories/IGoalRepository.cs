using goal_api.Models;

namespace goal_api.Repositories;

public interface IGoalRepository
{
    Task<IEnumerable<Goal>> GetAllAsync();
    Task<IEnumerable<Goal>> GetByMemberIdAsync(int memberId);
    Task<Goal?> GetByIdAsync(int id);
    Task<Goal> CreateAsync(int teamMemberId, string description);
    Task<Goal?> ToggleCompletedAsync(int id);
    Task<bool> DeleteAsync(int id);
}
