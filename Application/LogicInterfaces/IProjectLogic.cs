using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task CreateAsync(ProjectCreationDto dto);
    Task AddCollaboratorAsync(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaborators(int id);
    Task RemoveCollaborator(AddUserToProjectDto collaborator);
    
    Task AddUserStoryAsync(UserStoryDto dto);
    Task<List<UserStory>> GetUserStoriesAsync(int projectId);
    
    Task CreateSprint(SprintCreationDto dto);
    Task<List<Sprint>> GetSprintsByProjectId(int id);
}