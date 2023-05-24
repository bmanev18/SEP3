using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IUserStoryDao
{
    Task UpdateUserStoryPointsAsync(int id, int points);
    Task UpdateUserStoryStatusAsync(int userStoryId, string status);
    Task UpdateUserStoryPriorityAsync(int userStoryId, string priority);
    Task DeleteUserStory(int id);
    Task AddSprintTask(SprintTaskCreationDto dto);
    Task EditTask(SprintTask task);
    Task<List<SprintTask>> GetTasks(int id);
    Task DeleteTask(int id);
}