using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IProjectDao
{
    Task CreateAsync(ProjectCreationDto dto);
    Task AddCollaborator(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaborators(int id);
    Task RemoveCollaborator(AddUserToProjectDto collaborator);

    Task AddUserStoryAsync(UserStoryDto dto);
    Task<List<UserStory>> GetUserStoriesAsync(int projectId);
    
    Task CreateSprint(SprintCreationDto dto);
    Task<List<Sprint>> GetSprintsByProjectId(int id);
}