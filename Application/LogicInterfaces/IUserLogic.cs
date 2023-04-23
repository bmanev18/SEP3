using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task CreateAsync(UserCreationDto dto);
    Task<User>? GetByUsernameAsync(string username);

}