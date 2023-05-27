using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ISprintLogic
{
    Task<Sprint> GetSprintById(int id);
    Task RemoveSprint(int id);
    Task AddUserStoryToSprint(UserStoryToSprintDto dto);
    Task<List<UserStory>> GetUserStoriesFromSprint(int id);
   // public Task<List<UserStory>> GetOtherUserStories(int id);

    Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto);
}