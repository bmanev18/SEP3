using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IProjectDao
{
    Task CreateAsync(ProjectCreationDto dto);
    Task AddCollaborator(AddUserToProjectDto collaborator);
    Task<int> AddUserStory(UserStoryDto dto);
    Task<List<ProjectDto>> GetAllProjects(string username);
    Task<List<UserStory>> GetProductBacklog(int id);


    /*AddUserStory (UserStoryMessage) returns (ResponseWithID);
    rpc GetAllProjects (Username) returns (ProjectsResponse);
    rpc GetProductBacklog (Id) returns (ProductBacklogResponse);*/


}