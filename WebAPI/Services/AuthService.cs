using Application.DAOInterfaces;
using DataAccessClient;
using Microsoft.AspNetCore.Identity;
using Shared.Model;

namespace WebAPI.Services;

public class AuthService: IAuthService
{
    private readonly IUserDao _userDao;

    public AuthService(IUserDao dao)
    {
        _userDao = dao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await _userDao.GetByUsernameAsync(username);
        if (existingUser == null)
        {
            throw new Exception("User doesn't exist");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }
    
    public Task RegisterUser(User user)
    {
        Shared.DTOs.UserCreationDto dto = new Shared.DTOs.UserCreationDto
        {
            FirstName = user.firstname,
            Lastname = user.lastname,
            Password = user.Password,
            Role = user.Role,
            Username = user.Username
        };
        _userDao.CreateAsync(dto);
        return Task.CompletedTask;
    }
}