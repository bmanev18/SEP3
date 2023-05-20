using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserStoryService
{
    Task<IEnumerable<UserStory>> GetUserStoriesAsync(int? id = null);
    Task<int> CreateUserStory(UserStoryDto dto);


    // Task UpdateStatusAsync(UserStoryUpdateDto dto);
    Task UpdateUserStoryStatusAsync(int userStoryId, string status);

    Task RemoveAsync(int storyId);
    Task UpdateStoryPointsAsync(int userStoryId, int points);
}