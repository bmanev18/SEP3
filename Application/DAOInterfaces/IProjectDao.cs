using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IProjectDao
{
    Task CreateAsync(ProjectCreationDto dto);
    Task<int> AddCollaborator(AddUserToProjectDto collaborator);
    Task<int> AddUserStory(UserStoryDto dto);
    Task<List<ProjectDto>> GetAllProjects(string username);
    Task<List<UserStory>> GetProductBacklog(int id);
    Task<int> RemoveCollaborator(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaborators(int id);

    Task DeleteUserStory(int id);


    /*AddUserStory (UserStoryMessage) returns (ResponseWithID);
    rpc GetAllProjects (Username) returns (ProjectsResponse);
    rpc GetProductBacklog (Id) returns (ProductBacklogResponse);*/


}