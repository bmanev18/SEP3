using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto dto);
    Task<User>? GetByUsernameAsync(string username);

}