namespace Shared.DTOs;

public class SprintTaskCreationDto
{
    public int UserStoryId { get; set; }
    public string Assignee { get; set; }
    public string Body { get; set; }
    public int StoryPoint { get; set; }
    public string Status { get; set; }
}