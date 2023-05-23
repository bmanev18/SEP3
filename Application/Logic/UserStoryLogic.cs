using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class UserStoryLogic:IUserStoryLogic
{
    private readonly IUserStoryDao _storyDao;

    public UserStoryLogic(IUserStoryDao storyDao)
    {
        _storyDao = storyDao;
    }

    public async Task UpdateUserStoryPointsAsync(int id, int points)
    {
        await _storyDao.UpdateUserStoryPointsAsync(id, points);
    }

    public async Task DeleteUserStory(int id)
    {
        await _storyDao.DeleteUserStory(id);
    }

    public async Task AddTask(SprintTaskCreationDto dto)
    {
        await _storyDao.AddSprintTask(dto);
    }

    public async Task EditTask(SprintTask task)
    {
        await _storyDao.EditTask(task);
    }

    public async Task<List<SprintTask>> GetTasks(int id)
    {
        return await _storyDao.GetTasks(id);
    }

    public async Task RemoveTask(int id)
    {
        await _storyDao.DeleteTask(id);
    }
}