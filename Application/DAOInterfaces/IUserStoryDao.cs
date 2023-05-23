using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IUserStoryDao
{
    Task UpdateUserStoryPointsAsync(int id, int points);
    Task DeleteUserStory(int id);
    Task AddSprintTask(SprintTaskCreationDto dto);
    Task EditTask(SprintTask task);
    Task<List<SprintTask>> GetTasks(int id);
    Task DeleteTask(int id);
}