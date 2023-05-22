using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task CreateAsync(ProjectCreationDto dto);
    Task<List<ProjectDto>> GetAllProjects(string username);
    
    Task AddCollaboratorAsync(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaborators(int id);
    Task<int> RemoveCollaborator(AddUserToProjectDto collaborator);
    
    Task<int> AddUserStoryAsync(UserStoryDto dto);
    Task<List<UserStory>> GetUserStoriesAsync(int id);
    Task UpdateUserStoryPointsAsync(int userStoryId, int points);
    Task DeleteUserStory(int id);
    Task UpdateUserStoryStatusAsync(int userStoryId, string status);
    Task UpdateUserStoryPriorityAsync(int userStoryId, string priority);

}