using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserStoryService
{
    Task RemoveTaskAsync(int id, int storyId);
    Task RemoveAsync(int storyId);
    Task UpdateStoryPointsAsync(int id, int points);
    Task UpdateUserStoryStatusAsync(int id, string status);

    Task UpdateUserStoryPriorityAsync(int id, string priority);
    Task<IEnumerable<SprintTask>> GetTasksAsync(int id);

    Task CreateTaskAsync(SprintTaskCreationDto dto);
    Task UpdateTaskAsync(SprintTask? task);
}