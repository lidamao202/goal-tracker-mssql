namespace goal_api.DTOs;

public class CreateGoalRequest
{
    public int TeamMemberId { get; set; }
    public string Description { get; set; } = string.Empty;
}
