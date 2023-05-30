namespace Shared.Model;

public class SprintTask
{
    public required int Id { get; init; }
    public required int UserStoryId { get; init; }
    public required string Assignee { get; set; }
    public required string Body { get; set; }
    public required int StoryPoint { get; set; }
    public required string Status { get; set; }
}