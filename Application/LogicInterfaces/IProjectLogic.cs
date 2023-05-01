using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task CreateAsync(ProjectCreationDto dto);
    Task AddCollaboratorAsync(AddUserToProjectDto collaborator);
    Task<int> AddUserStoryAsync(UserStoryDto dto);
    Task<List<ProjectDto>> GetAllProjects(string username);
    Task<List<UserStory>> GetProductBacklog(int id);
}