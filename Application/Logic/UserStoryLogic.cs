using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class UserStoryLogic : IUserStoryLogic
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

    public async Task UpdateUserStoryStatusAsync(int storyId, string status)
    {
        await _storyDao.UpdateUserStoryStatusAsync(storyId, status);
    }

    public async Task UpdateUserStoryPriorityAsync(int storyId, string priority)
    {
        await _storyDao.UpdateUserStoryPriorityAsync(storyId, priority);
    }

    public async Task DeleteUserStoryAsync(int storyId)
    {
        await _storyDao.DeleteUserStoryAsync(storyId);
    }

    public async Task AddTaskAsync(TaskCreationDto dto)
    {
        await _storyDao.AddTaskAsync(dto);
    }

    public async Task EditTaskAsync(TaskClass taskClass)
    {
        await _storyDao.EditTaskAsync(taskClass);
    }

    public async Task<List<TaskClass>> GetTasksAsync(int storyId)
    {
        return await _storyDao.GetTasksAsync(storyId);
    }

    public async Task RemoveTaskAsync(int storyId)
    {
        await _storyDao.DeleteTaskAsync(storyId);
    }
}