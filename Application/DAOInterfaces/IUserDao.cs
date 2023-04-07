using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(UserCreationDto dto);
    Task<User?> GetByUsernameAsync(string username);
    
}