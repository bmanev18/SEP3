using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IUserStoryDao
{
    Task<List<SprintTask>> GetTasks(int id);
    Task RemoveTask(int id);
    Task AddSprintTask(SprintTaskCreationDto dto);
    Task DeleteUserStory(int id);
    Task UpdateUserStoryPointsAsync(int id, int points);
}