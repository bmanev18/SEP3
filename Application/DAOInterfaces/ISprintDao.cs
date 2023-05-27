using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface ISprintDao
{
    Task<Sprint> GetSprintByIdAsync(int id);
    Task RemoveSprintAsync(int id);
    Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto);
    Task<List<UserStory>> GetUserStoriesFromSprintAsync(int id);
     //Task<List<UserStory>> GetOtherUserStoriesAsync(int id);

    Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto);
}