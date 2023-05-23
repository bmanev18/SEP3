
using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<IEnumerable<Project>> GetProjectsByUsernameAsync(string? username);
    
    Task<List<UserFinderDto>> LookForUsersAsync(string usernameContains);


}