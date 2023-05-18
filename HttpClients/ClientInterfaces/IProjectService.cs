using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IProjectService
{
    Task<Project> Create(ProjectCreationDto dto);
    
    Task<IEnumerable<Project>> GetProjectsByUsername(string? nameContains = null);

    Task AddCollaborator(int projectId, string username, List<UserFinderDto> users, string role);

    Task<List<UserFinderDto>> GetAllCollaborators(int id);

    Task RemoveCollaborator(string username, int projectid);
    

}