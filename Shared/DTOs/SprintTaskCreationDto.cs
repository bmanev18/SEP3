namespace Shared.DTOs;

public class SprintTaskCreationDto
{
    public int UserStoryId { get; set; }
    public string Assignee { get; set; }
    public string Body { get; set; }
    public int StoryPoint { get; set; }
    // public bool Status { get; set; }
    public SprintTaskCreationDto(int userStoryId, string assignee, string body, int storyPoint)
    {
        UserStoryId = userStoryId;
        Assignee = assignee;
        Body = body;
        StoryPoint = storyPoint;
    }
}