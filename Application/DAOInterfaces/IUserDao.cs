using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IUserDao
{
    Task CreateAsync(UserCreationDto dto);
    Task<User> GetUserByUsernameAsync(LoginDto loginDto);

    Task<List<Project>> GetProjectsAsync(string username);
    Task<List<UserFinderDto>> LookForUsersAsync(string username);
}