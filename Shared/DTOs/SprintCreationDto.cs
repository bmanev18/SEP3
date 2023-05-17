namespace Shared.DTOs;

public class SprintCreationDto
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string StartDate { get; set; }
    public string? EndDate { get; set; }
}