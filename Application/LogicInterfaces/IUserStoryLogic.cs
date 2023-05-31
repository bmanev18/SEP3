using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IUserStoryLogic
{
    Task UpdateUserStoryPointsAsync(int id, int points);
    Task UpdateUserStoryStatusAsync(int storyId, string status);
    Task UpdateUserStoryPriorityAsync(int storyId, string priority);
    Task DeleteUserStoryAsync(int storyId);

    Task AddTaskAsync(TaskCreationDto dto);
    Task EditTaskAsync(TaskClass taskClass);

    Task<List<TaskClass>> GetTasksAsync(int storyId);
    Task RemoveTaskAsync(int storyId);
}