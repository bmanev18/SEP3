namespace Shared.Model;

public class TaskClass
{
    public required int Id { get; init; }
    public required int UserStoryId { get; init; }
    public required string Assignee { get; set; }
    public required string Body { get; set; }
    public required int StoryPoints { get; set; }
    public required string Status { get; set; }
}