namespace Shared.DTOs;

public class UserStoryDto
{
    public int Project_id { get; set; }
    public string Body { get; set; }
    public string Priority { get; set; }
    public string? Status { get; set; }
    public int StoryPoints { get; set; }
}