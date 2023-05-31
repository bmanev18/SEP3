using System.ComponentModel.DataAnnotations;
using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserDao _userDao;

    public AuthService(IUserDao dao)
    {
        _userDao = dao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) throw new Exception("Empty Fields");

        var loginDto = new LoginDto
        {
            Username = username,
            Password = password
        };

        var existingUser = await _userDao.GetUserByUsernameAsync(loginDto);


        if (existingUser == null) throw new Exception("User doesn't exist");

        if (!existingUser.Password.Equals(password)) throw new Exception("Password mismatch");

        return await Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        ValidateRegistration(user);
        var dto = new UserCreationDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Role = user.Role,
            Username = user.Username
        };

        _userDao.CreateAsync(dto);
        return Task.CompletedTask;
    }

    private void ValidateRegistration(User user)
    {
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.FirstName) ||
            string.IsNullOrEmpty(user.LastName)) throw new ValidationException("All Fields Are Mandatory!");

        if (user.Password.Length < 8) throw new ValidationException("Password must be at least 8 characters!");
    }
}