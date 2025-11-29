using Dapper;
using goal_api.Data;
using goal_api.Models;

namespace goal_api.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly DapperContext _context;

    public GoalRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Goal>> GetAllAsync()
    {
        const string sql = "SELECT Id, TeamMemberId, Description, IsCompleted, CreatedAt FROM Goals ORDER BY CreatedAt";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Goal>(sql);
    }

    public async Task<IEnumerable<Goal>> GetByMemberIdAsync(int memberId)
    {
        const string sql = "SELECT Id, TeamMemberId, Description, IsCompleted, CreatedAt FROM Goals WHERE TeamMemberId = @MemberId ORDER BY CreatedAt";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Goal>(sql, new { MemberId = memberId });
    }

    public async Task<Goal?> GetByIdAsync(int id)
    {
        const string sql = "SELECT Id, TeamMemberId, Description, IsCompleted, CreatedAt FROM Goals WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Goal>(sql, new { Id = id });
    }

    public async Task<Goal> CreateAsync(int teamMemberId, string description)
    {
        const string sql = @"
            INSERT INTO Goals (TeamMemberId, Description, IsCompleted, CreatedAt)
            VALUES (@TeamMemberId, @Description, 0, GETUTCDATE());
            SELECT Id, TeamMemberId, Description, IsCompleted, CreatedAt
            FROM Goals WHERE Id = SCOPE_IDENTITY()";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<Goal>(sql, new { TeamMemberId = teamMemberId, Description = description });
    }

    public async Task<Goal?> ToggleCompletedAsync(int id)
    {
        const string sql = @"
            UPDATE Goals SET IsCompleted = CASE WHEN IsCompleted = 1 THEN 0 ELSE 1 END WHERE Id = @Id;
            SELECT Id, TeamMemberId, Description, IsCompleted, CreatedAt FROM Goals WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Goal>(sql, new { Id = id });
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Goals WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        var affected = await connection.ExecuteAsync(sql, new { Id = id });
        return affected > 0;
    }
}
