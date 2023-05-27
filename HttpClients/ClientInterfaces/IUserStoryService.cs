using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserStoryService
{
    Task RemoveTask(int id, int storyId);
    
    //Task UpdateAsync(UserStoryUpdateDto dto);

    Task RemoveAsync(int storyId);
    Task UpdateStoryPointsAsync(int id, int points);
    Task UpdateUserStoryStatusAsync(int id, string status);

    Task UpdateUserStoryPriorityAsync(int id, string priority);
    Task<IEnumerable<SprintTask>> GetTasks(int id);
    
    Task CreateTask(SprintTaskCreationDto dto);
    Task UpdateTask(SprintTask task);
    
}