using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface ISprintService
{
    Task RemoveStory(int sprintId, int storyId);
    public Task<IEnumerable<UserStory>> GetUserStoriesFromSprint(int sprintid);
    public Task<IEnumerable<UserStory>> OtherUserStories(int projectid);

    public Task AddUserStory(int sprintid, int storyid);
    
    
    // Tasks
    Task CreateTask(SprintTaskCreationDto dto);



    
}