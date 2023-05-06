using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class ProjectLogic : IProjectLogic
{
    private readonly IProjectDao _projectDao;

    public ProjectLogic(IProjectDao projectDao)
    {
        this._projectDao = projectDao;
    }


    public async Task CreateAsync(ProjectCreationDto dto)
    {
        await _projectDao.CreateAsync(dto);
    }

    public async Task AddCollaboratorAsync(AddUserToProjectDto collaborator)
    {
        await _projectDao.AddCollaborator(collaborator);
    }
    
    public async Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        return await _projectDao.GetAllCollaborators(id);
    }

    public async Task<int> RemoveCollaborator(AddUserToProjectDto collaborator)
    {
       return await _projectDao.RemoveCollaborator(collaborator);
       
    }


    public async Task<int> AddUserStoryAsync(UserStoryDto dto)
    {
        return await _projectDao.AddUserStory(dto);
    }

    public async Task<List<ProjectDto>> GetAllProjects(string username)
    {
        return await _projectDao.GetAllProjects(username);
    }

    public async Task<List<UserStory>> GetProductBacklog(int id)
    {
        return await _projectDao.GetProductBacklog(id);
    }
}
