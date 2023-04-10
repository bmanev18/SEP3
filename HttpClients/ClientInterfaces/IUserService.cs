
using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
    
    
}