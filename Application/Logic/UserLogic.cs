using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<List<ProjectDto>> GetProjectsAsync(string username)
    {
        return await _userDao.GetProjects(username);
    }

    public async Task<List<UserFinderDto>> LookForUsersAsync(string username)
    {
        return await _userDao.LookForUsers(username);
    }
}