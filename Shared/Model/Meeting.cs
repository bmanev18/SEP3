namespace Shared.Model;

public class Meeting
{
    public required int ProjectId { get; set; }
    public required string Title { get; init; }
    public required string Note { get; init; }
    public required string Author { get; init; }
}