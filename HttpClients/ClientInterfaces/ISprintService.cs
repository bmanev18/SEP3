using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface ISprintService
{
    public Task<Sprint> GetSprintById(int sprintId);
    public Task DeleteSprint(int sprintId);
    public Task AddUserStoryToSprint(int sprintId, int storyId);
    public Task<IEnumerable<UserStory>> GetUserStoriesFromSprint(int sprintId);
    Task RemoveStoryFromSprint(int sprintId, int storyId);


    // public Task<IEnumerable<UserStory>> OtherUserStories(int projectid);


    // Tasks
    //Task CreateTask(SprintTaskCreationDto dto);



    
}