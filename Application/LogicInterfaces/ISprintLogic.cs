using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ISprintLogic
{
    Task<Sprint> GetSprintById(int id);
    
    Task EditTask(SprintTask task);
    Task RemoveSprint(SprintRemovalDto dto);

    Task AddUserStoryToSprint(UserStoryToSprintDto dto);
    Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto);

    Task<List<UserStory>> GetAllUserStoriesFromSprint(int id);
    

}