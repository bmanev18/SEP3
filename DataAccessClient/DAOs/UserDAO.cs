using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using UserMessage = DataAccessClient.UserMessage;
using LoginDto = Shared.DTOs.LoginDto;
using ProjectDto = Shared.DTOs.ProjectDto;

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


    public async Task CreateAsync(UserCreationDto dto)
    {
        var request = new UserMessage
        {
            Password = dto.Password,
            Username = dto.Username,
            Role = dto.Role,
            FirstName = dto.FirstName,
            LastName = dto.Lastname
        };
        var call = await _client.CreateUserAsync(request);
        if (!call.Response_)
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
        if (string.IsNullOrEmpty(call.Username))
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
        /*if (projectsResponse.Projects.Count==0)
        {
            throw new InvalidOperationException("No projects were found");
        }*/

        var list = projectsResponse.Projects.Select(project => new ProjectDto { Id = project.Id, Title = project.Title, }).ToList();

        return Task.FromResult(list);
    }

    public Task<List<UserFinderDto>> LookForUsers(string username)
    {
        var request = new Username
        {
            Username_ = username
        };
        var call = _client.LookForUsers(request);
        /*if (call.Users.Count == 0)
        {
            throw new InvalidOperationException("No users were found");
        }*/
        var list = call.Users.Select(user => new UserFinderDto { Username = user.Username, FirstName = user.FirstName, LastName = user.LastName, Role = user.Role }).ToList();

        return Task.FromResult(list);
    }
}