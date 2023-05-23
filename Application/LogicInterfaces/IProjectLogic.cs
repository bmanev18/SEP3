using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task CreateAsync(ProjectCreationDto dto);
    Task AddCollaboratorAsync(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaborators(int id);
    Task<int> RemoveCollaborator(AddUserToProjectDto collaborator);
    
    Task<int> AddUserStoryAsync(UserStoryDto dto);
    Task<List<UserStory>> GetUserStoriesAsync(int projectId);
    Task CreateSprint(SprintCreationDto dto, int id);
    
    Task<List<Sprint>> GetSprintsByProjectId(int id);
    
}