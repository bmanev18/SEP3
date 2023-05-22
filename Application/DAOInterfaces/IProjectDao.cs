using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IProjectDao
{
    Task CreateAsync(ProjectCreationDto dto);
    Task CreateSprint(SprintCreationDto dto, int id);
    
    Task AddCollaborator(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaborators(int id);
    Task<int> RemoveCollaborator(AddUserToProjectDto collaborator);
    
    Task<int> AddUserStory(UserStoryDto dto);
    Task<List<UserStory>> GetUserStoriesAsync(int projectId);
    Task<List<Sprint>> GetSprintsByProjectId(int id);
}