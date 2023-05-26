using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface ISprintService
{
    Task RemoveStory(int sprintId, int storyId);
    
    
    // Tasks
    Task CreateTask(SprintTaskCreationDto dto);



    
}