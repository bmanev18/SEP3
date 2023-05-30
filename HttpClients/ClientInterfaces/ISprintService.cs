using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface ISprintService
{
    public Task<Sprint> GetSprintByIdAsync(int sprintId);
    public Task DeleteSprintAsync(int sprintId);
    public Task AddUserStoryToSprintAsync(int sprintId, int storyId);
    public Task<IEnumerable<UserStory>> GetUserStoriesFromSprintAsync(int sprintId);
    Task RemoveStoryFromSprintAsync(int sprintId, int storyId);
}