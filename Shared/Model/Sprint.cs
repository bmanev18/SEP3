namespace Shared.Model;

public class Sprint
{
    public required int Id { get; init; }
    public required int ProjectId { get; set; }
    public required string Name { get; set; }
    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
}