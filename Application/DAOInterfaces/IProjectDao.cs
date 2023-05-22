using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IProjectDao
{
    Task CreateAsync(ProjectCreationDto dto);
    Task<List<ProjectDto>> GetAllProjects(string username);
    
    Task<int> AddCollaborator(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaborators(int id);
    Task<int> RemoveCollaborator(AddUserToProjectDto collaborator);
    
    Task<int> AddUserStory(UserStoryDto dto);
    Task<List<UserStory>> GetUserStoriesAsync(int id);
    Task UpdateUserStoryPointsAsync(int userStoryId, int points);
    Task UpdateUserStoryStatusAsync(int userStoryId, string status);
    Task UpdateUserStoryPriorityAsync(int userStoryId, string priority);


    Task DeleteUserStory(int id);
}