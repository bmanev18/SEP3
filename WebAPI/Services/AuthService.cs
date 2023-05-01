using Application.DAOInterfaces;
using DataAccessClient;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Services;

public class AuthService: IAuthService
{
    private readonly IUserDao _userDao;

    private readonly IList<User> users = new List<User>
    {
        new User
        {
            Username = "test",
            Password = "testtest",
            Role = "1",
            Lastname = "test",
            Firstname = "test"
        }
    };

    public AuthService(IUserDao dao)
    {
        _userDao = dao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        
        LoginDto loginDto = new LoginDto
        {
            Username = username,
            Password = password
        };

        User? existingUser =  await _userDao.GetUserByUsername(loginDto);
        /*User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));*/
        // string realPassword = await _userDao.GetUserPassword(loginDto);


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
            FirstName = user.Firstname,
            Lastname = user.Lastname,
            Password = user.Password,
            Role = user.Role,
            Username = user.Username
        };
        _userDao.CreateAsync(dto);
        return Task.CompletedTask;
    }
}