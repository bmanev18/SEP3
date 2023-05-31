using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ISprintLogic
{
    Task<Sprint> GetSprintByIdAsync(int sprintId);
    Task RemoveSprintAsync(int sprintId);

    Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto);
    Task<List<UserStory>> GetUserStoriesFromSprintAsync(int sprintId);
    Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto);
}