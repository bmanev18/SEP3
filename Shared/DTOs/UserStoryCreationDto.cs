namespace Shared.DTOs;

public class UserStoryCreationDto
{
    public required int ProjectId { get; init; }
    public required string Body { get; init; }
    public required string Priority { get; init; }
    public required int StoryPoints { get; init; }
}