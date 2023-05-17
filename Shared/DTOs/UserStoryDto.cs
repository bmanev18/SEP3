namespace Shared.DTOs;

public class UserStoryDto
{
    public int Project_id { get; set; }
    public string Body { get; set; }
    public string Priority { get; set; }
    public bool Status { get; set; }
    public int StoryPoints { get; set; }
}