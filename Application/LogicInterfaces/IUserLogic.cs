using Shared.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<List<Project>> GetProjectsAsync(string username);
    Task<List<UserFinderDto>> LookForUsersAsync(string username);
}