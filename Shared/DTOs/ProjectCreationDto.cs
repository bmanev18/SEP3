using Shared.Model;

namespace Shared.DTOs;

public class ProjectCreationDto
{
    public required string Name { get; init; }
    public required string OwnerUsername { get; init; }
}