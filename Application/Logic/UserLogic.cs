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
    
    
    public async Task CreateAsync(UserCreationDto dto)
    {
        await _userDao.CreateAsync(dto);
    }

    public Task<User>? GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}