using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IProjectService
{
    Task<Project> Create(ProjectCreationDto dto);
    
    Task<IEnumerable<Project>> GetProjects(string? nameContains = null);

}