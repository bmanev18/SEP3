using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserStoryService
{
    Task RemoveTask(int id);
    
    Task UpdateAsync(UserStoryUpdateDto dto);

    Task RemoveAsync(int storyId);
    Task UpdateStoryPointsAsync(int userStoryId, int points);
    Task UpdateUserStoryStatusAsync(int userStoryId, string status);

    Task UpdateUserStoryPriorityAsync(int userStoryId, string priority);
    Task<IEnumerable<SprintTask>> GetTasks(int id);
    
    Task CreateTask(SprintTaskCreationDto dto);
    
}