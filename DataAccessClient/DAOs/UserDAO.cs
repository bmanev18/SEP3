using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using UserCreationDto = DataAccessClient.UserCreationDto;
using LoginDto = Shared.DTOs.LoginDto;

namespace DataAccess.DAOs;

public class UserDao : IUserDao
{
    private readonly UserAccess.UserAccessClient _client;

    public UserDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new UserAccess.UserAccessClient(channel);
    }


    public async Task CreateAsync(Shared.DTOs.UserCreationDto dto)
    {
        var request = new UserCreationDto
        {
            Password = dto.Password,
            Username = dto.Username,
            Role = dto.Role,
            FirstName = dto.FirstName,
            LastName = dto.Lastname
        };
        var call = await _client.CreateUserAsync(request);
        if (call.Code != 200)
        {
            throw new InvalidOperationException("User wasn't created");
        }
    }

    public Task<User> GetUserByUsername(LoginDto loginDto)
    {
        var username = new Username
        {
            Username_ = loginDto.Username
        };
        var call = _client.UserByUsername(username);
        if (call == null)
        {
            throw new InvalidOperationException("No User found!");
        }

        var result = new User
        {
            Username = call.Username,
            Firstname = call.Username,
            Lastname = call.LastName,
            Password = call.Password,
            Role = call.Role
        };
        return Task.FromResult(result);
    }

    public Task<List<ProjectDto>> GetProjects(string username)
    {
        var projectsResponse = _client.GetAllProjects(new Username { Username_ = username });
        var list = new List<ProjectDto>();
        foreach (var project in projectsResponse.Projects)
        {
            list.Add(new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
            });
        }

        return Task.FromResult(list);
    }

    public Task<List<UserFinderDto>> LookForUsers(string username)
    {
        var request = new Username
        {
            Username_ = username
        };
        var call = _client.LookForUsers(request);
        List<UserFinderDto> list = new();
        foreach (var user in call.Users)
        {
            list.Add(new UserFinderDto { Username = user.Username, FirstName = user.FirstName, LastName = user.LastName, Role = user.Role });
        }

        return Task.FromResult(list);
    }
}