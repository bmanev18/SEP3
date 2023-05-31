namespace Shared.DTOs;

public class SprintCreationDto
{
    public required int ProjectId { get; init; }
    public required string Name { get; init; }
    public required string StartDate { get; init; }
    public required string? EndDate { get; init; }
}