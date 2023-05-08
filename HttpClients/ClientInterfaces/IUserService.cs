
using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    
    Task<List<UserFinderDto>> LookForUsers(string usernameContains);


}