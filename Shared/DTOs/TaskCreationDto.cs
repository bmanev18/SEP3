namespace Shared.DTOs;

public class TaskCreationDto
{
    public required int UserStoryId { get; init; }
    public required string Assignee { get; init; }
    public required string Body { get; init; }
    public required int StoryPoints { get; init; }
}