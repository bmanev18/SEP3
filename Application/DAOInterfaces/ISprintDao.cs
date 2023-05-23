using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface ISprintDao
{
    Task<Sprint> GetSprintById(int id);
    Task RemoveSprint(int id);
    Task AddUserStoryToSprint(UserStoryToSprintDto dto);
    Task<List<UserStory>> GetUserStoriesFromSprint(int id);
    Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto);
}