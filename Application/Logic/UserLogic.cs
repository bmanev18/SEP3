using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public async Task<List<Project>> GetProjectsAsync(string username)
    {
        return await _userDao.GetProjectsAsync(username);
    }

    public async Task<List<UserFinderDto>> LookForUsersAsync(string username)
    {
        return await _userDao.LookForUsersAsync(username);
    }
}