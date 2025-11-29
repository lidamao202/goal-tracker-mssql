using Dapper;
using goal_api.Data;
using goal_api.Models;

namespace goal_api.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly DapperContext _context;

    public TeamMemberRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamMember>> GetAllAsync()
    {
        const string sql = "SELECT Id, Name, Mood, CreatedAt FROM TeamMembers ORDER BY Name";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TeamMember>(sql);
    }

    public async Task<TeamMember?> GetByIdAsync(int id)
    {
        const string sql = "SELECT Id, Name, Mood, CreatedAt FROM TeamMembers WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<TeamMember>(sql, new { Id = id });
    }

    public async Task<TeamMember?> UpdateMoodAsync(int id, Mood mood)
    {
        const string sql = @"
            UPDATE TeamMembers SET Mood = @Mood WHERE Id = @Id;
            SELECT Id, Name, Mood, CreatedAt FROM TeamMembers WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<TeamMember>(sql, new { Id = id, Mood = (int)mood });
    }
}
