using Microsoft.AspNetCore.Mvc;
using goal_api.DTOs;
using goal_api.Models;
using goal_api.Services;

namespace goal_api.Controllers;

[ApiController]
[Route("api")]
public class GoalTrackerController : ControllerBase
{
    private readonly IGoalTrackerService _service;

    public GoalTrackerController(IGoalTrackerService service)
    {
        _service = service;
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardResponse>> GetDashboard()
    {
        var (members, stats) = await _service.GetDashboardAsync();
        return Ok(new DashboardResponse { Members = members, Stats = stats });
    }

    [HttpGet("members")]
    public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetMembers()
    {
        var members = await _service.GetMembersAsync();
        return Ok(members);
    }

    [HttpPost("goals")]
    public async Task<ActionResult<GoalDto>> CreateGoal([FromBody] CreateGoalRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Description))
        {
            return BadRequest(new { message = "Goal description is required" });
        }

        if (request.Description.Length > 200)
        {
            return BadRequest(new { message = "Goal description must be 200 characters or less" });
        }

        var goal = await _service.CreateGoalAsync(request.TeamMemberId, request.Description);
        if (goal == null)
        {
            return NotFound(new { message = "Team member not found" });
        }

        return Created($"/api/goals/{goal.Id}", goal);
    }

    [HttpPatch("goals/{id}/toggle")]
    public async Task<ActionResult<GoalDto>> ToggleGoal(int id)
    {
        var goal = await _service.ToggleGoalAsync(id);
        if (goal == null)
        {
            return NotFound(new { message = "Goal not found" });
        }

        return Ok(goal);
    }

    [HttpDelete("goals/{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var deleted = await _service.DeleteGoalAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = "Goal not found" });
        }

        return NoContent();
    }

    [HttpPatch("members/{id}/mood")]
    public async Task<ActionResult<TeamMemberDto>> UpdateMood(int id, [FromBody] UpdateMoodRequest request)
    {
        if (!Enum.IsDefined(typeof(Mood), request.Mood))
        {
            return BadRequest(new { message = "Invalid mood value" });
        }

        var member = await _service.UpdateMoodAsync(id, request.Mood);
        if (member == null)
        {
            return NotFound(new { message = "Team member not found" });
        }

        return Ok(member);
    }
}
