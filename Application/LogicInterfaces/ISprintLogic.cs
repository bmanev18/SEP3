using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ISprintLogic
{
    Task CreateSprint(SprintCreationDto dto);

    Task<Sprint> GetSprintById(int id);
    Task<List<Sprint>> GetSprintsByProjectId(int id);
    Task RemoveSprint(SprintRemovalDto dto);

    Task AddSprintTask(SprintTaskCreationDto dto);

    Task<List<SprintTask>> GetTasks(int id);

    Task AssignSprintTask(string username, int id);
    Task RemoveTask(int id);

    Task AddUserStoryToSprint(UserStoryToSprintDto dto);
    Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto);

    Task<List<UserStory>> GetAllUserStoriesFromSprint(int id);

}