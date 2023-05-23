using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IUserDao
{
    Task CreateAsync(UserCreationDto dto);
    Task<User> GetUserByUsername(LoginDto loginDto);
    
    Task<List<ProjectDto>> GetProjects(string username);
    Task<List<UserFinderDto>> LookForUsers(string username);
}