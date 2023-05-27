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

    public async Task UpdateUserStoryStatusAsync(int userStoryId, string status)
    {
        await _storyDao.UpdateUserStoryStatusAsync(userStoryId, status);
    }

    public async Task UpdateUserStoryPriorityAsync(int userStoryId, string priority)
    {
        await _storyDao.UpdateUserStoryPriorityAsync(userStoryId, priority);
    }

    public async Task DeleteUserStory(int id)
    {
        await _storyDao.DeleteUserStoryAsync(id);
    }

    public async Task AddTask(SprintTaskCreationDto dto)
    {
        await _storyDao.AddSprintTaskAsync(dto);
    }

    public async Task EditTask(SprintTask task)
    {
        await _storyDao.EditTaskAsync(task);
    }

    public async Task<List<SprintTask>> GetTasks(int id)
    {
        return await _storyDao.GetTasksAsync(id);
    }

    public async Task RemoveTask(int id)
    {
        await _storyDao.DeleteTaskAsync(id);
    }
}