using goal_api.Models;

namespace goal_api.DTOs;

public class MoodCountDto
{
    public Mood Mood { get; set; }
    public int Count { get; set; }
}
