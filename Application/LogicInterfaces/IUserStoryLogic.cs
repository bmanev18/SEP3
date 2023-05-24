using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IUserStoryLogic
{
    Task UpdateUserStoryPointsAsync(int id, int points);
    Task UpdateUserStoryStatusAsync(int userStoryId, string status);
    Task UpdateUserStoryPriorityAsync(int userStoryId, string priority);
    Task DeleteUserStory(int id);
    Task AddTask(SprintTaskCreationDto dto);
    Task EditTask(SprintTask task);
    Task<List<SprintTask>> GetTasks(int id);
    Task RemoveTask(int id);
}