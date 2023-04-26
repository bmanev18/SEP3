
using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Microsoft.VisualBasic.CompilerServices;
using Shared.DTOs;
using Shared.Model;
using UserCreationDto = DataAccessClient.UserCreationDto;

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
    

    public async Task<User?> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}