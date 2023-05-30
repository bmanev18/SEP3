namespace Shared.DTOs;

public class SprintTaskCreationDto
{
    public required int UserStoryId { get; init; }
    public required string Assignee { get; init; }
    public required string Body { get; init; }
    public required int StoryPoint { get; init; }
}