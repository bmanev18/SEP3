using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IProjectService
{
    Task<Project> Create(ProjectCreationDto dto);
    
    Task<IEnumerable<Project>> GetProjectsByUsername(string? nameContains = null);

    Task addCollaborator(int projectId, string username);

}