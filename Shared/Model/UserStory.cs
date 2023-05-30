namespace Shared.Model;

public class UserStory
{
    public required int Id { get; init; }
    public required int ProjectId { get; init; }
    public required string Body { get; init; }
    public required string Priority { get; set; }
    public required string Status { get; set; }
    public required int StoryPoints { get; set; }
}