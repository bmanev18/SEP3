using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface ISprintService
{

    Task CreateSprint(SprintCreationDto dto);
    Task<IEnumerable<Sprint>> GetSprintsAsync(int? projectId = null);

    Task RemoveStory(int sprintId, int storyId);
    
    
    // Tasks
    Task<IEnumerable<SprintTask>> GetTasks(int? storyId = null);

    Task CreateTask(SprintTaskCreationDto dto);

    Task UpdateTask(SprintTask task);

    Task RemoveTask(int taskId);
}