using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface ISprintDao
{
    Task CreateSprint(SprintCreationDto dto);

    Task<Sprint> GetSprintById(int id);
    Task<List<Sprint>> GetSprintsByProjectId(int id);
    Task RemoveSprint(SprintRemovalDto dto);

    Task AddSprintTask(SprintTaskCreationDto dto);

    Task<List<SprintTask>> GetTasks(int id);

    Task EditTask(SprintTask task);

    Task AddUserStoryToSprint(UserStoryToSprintDto dto);
    Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto);

    Task RemoveTask(int id);

    Task<List<UserStory>> GetAllUserStoriesFromSprint(int id);
}