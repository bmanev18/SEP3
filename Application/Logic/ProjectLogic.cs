using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class ProjectLogic : IProjectLogic
{
    private readonly IProjectDao projectDao;

    public ProjectLogic(IProjectDao projectDao)
    {
        this.projectDao = projectDao;
    }


    public async Task CreateAsync(ProjectCreationDto dto)
    {
        await projectDao.CreateAsync(dto);
    }
    
    public async Task AddCollaboratorAsync(AddUserToProjectDto collaborator)
    {
        await projectDao.AddCollaborator(collaborator);
    }
    public async Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        return await projectDao.GetAllCollaborators(id);
    }

    public async Task<int> RemoveCollaborator(AddUserToProjectDto collaborator)
    {
       return await projectDao.RemoveCollaborator(collaborator);   
    }
    public async Task CreateSprint(SprintCreationDto dto, int id)
    {
        await projectDao.CreateSprint(dto, id);
    }

    public async Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
      return await projectDao.GetSprintsByProjectId(id);
    }

    public async Task<int> AddUserStoryAsync(UserStoryDto dto)
    {
        return await projectDao.AddUserStory(dto);
    }
    

    public async Task<List<UserStory>> GetUserStoriesAsync(int id)
    {
        return await projectDao.GetUserStoriesAsync(id);
    }
}