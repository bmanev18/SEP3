
using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Microsoft.VisualBasic.CompilerServices;
using Shared.DTOs;
using Shared.Model;
using UserCreationDto = DataAccessClient.UserDto;
using LoginDto = Shared.DTOs.LoginDto;

namespace DataAccess.DAOs;

public class UserDAO : IUserDao
{
    private UserAccess.UserAccessClient client;

    public UserDAO()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        client = new UserAccess.UserAccessClient(channel);
    }


    public async Task CreateAsync(Shared.DTOs.UserCreationDto dto)
    {
        UserCreationDto request = new UserCreationDto
        {
            Password = dto.Password,
            Username = dto.Username,
            RoleId = dto.Role,
            FirstName = dto.FirstName,
            LastName = dto.Lastname
        };
        var call = client.CreateUser(request);
    }
    

    public async Task<User?> GetByUsernameAsync(LoginDto loginDto)
    {
        UserLoginDto userLoginDto = new UserLoginDto
        {
            Username = loginDto.Username,
            Password = loginDto.Password
        };

        var call = client.Login(userLoginDto);
        User user = new User
        {
            Username = call.User.Username,
            Password = call.User.Password,
            Firstname = call.User.FirstName,
            Lastname = call.User.LastName,
            Role = call.User.RoleId
        };

        return user;
    }
}