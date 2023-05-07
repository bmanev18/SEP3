using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Microsoft.VisualBasic.CompilerServices;
using Shared.DTOs;
using Shared.Model;
using UserCreationDto = DataAccessClient.UserCreationDto;
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
            Role = dto.Role,
            FirstName = dto.FirstName,
            LastName = dto.Lastname
        };
        var call = client.CreateUser(request);
    }
    
    public Task<User> GetUserByUsername(LoginDto loginDto)
    {
        Username username = new Username
        {
            Username_ = loginDto.Username
        };
        var call = client.UserByUsername(username);
        User result = new User
        {
            Username = call.Username,
            Firstname = call.Username,
            Lastname = call.LastName,
            Password = call.Password,
            Role = call.Role
        };
        return Task.FromResult(result);
    }

    public Task<List<UserFinderDto>> LookForUsers(string username)
    {
        Username request = new Username
        {
            Username_ = username
        };
        var call = client.LookForUsers(request);
        List<UserFinderDto> list = new();
        foreach (var user in call.Users)
        {
            list.Add(new UserFinderDto
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            });
        }

        return Task.FromResult(list);
    }
}