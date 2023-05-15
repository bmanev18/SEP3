using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class SprintLogic : ISprintLogic
{
    private readonly ISprintDao _sprintDao;
    
    public SprintLogic(ISprintDao _sprintDao)
    {
        this._sprintDao = _sprintDao;
    }
    
    
    public async Task CreateSprint(SprintCreationDto dto)
    {
        await _sprintDao.CreateSprint(dto);
    }

    public async Task<Sprint> GetSprintById(int id)
    {
        return await _sprintDao.GetSprintById(id);
    }

    public async Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
        return await _sprintDao.GetSprintsByProjectId(id);
    }
    

    public async Task RemoveSprint(SprintRemovalDto dto)
    {
        await _sprintDao.RemoveSprint(dto);
    }

    public async Task AddSprintTask(SprintTaskCreationDto dto)
    {
        await _sprintDao.AddSprintTask(dto);
    }

    public async Task<List<SprintTask>> GetTasks(int id)
    {
        return await _sprintDao.GetTasks(id);
    }
    

    public async Task EditTask(SprintTask task)
    {
        await _sprintDao.EditTask(task);
    }

    public async Task AddUserStoryToSprint(UserStoryToSprintDto dto)
    {
        await _sprintDao.AddUserStoryToSprint(dto);
    }

    public async Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto)
    {
        await _sprintDao.RemoveUserStoryFromSprint(dto);
    }

    public async Task<List<UserStory>> GetAllUserStoriesFromSprint(int id)
    {
        return await _sprintDao.GetAllUserStoriesFromSprint(id);
    }
    public async Task RemoveTask(int id)
    {
         await _sprintDao.RemoveTask(id);
    }
    
}