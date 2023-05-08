using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserStoryService
{
    Task<int> createUserStory(UserStory dto);

    Task<IEnumerable<UserStory>> getUserStory(int id);
}