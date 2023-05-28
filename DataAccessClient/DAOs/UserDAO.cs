using Application.DAOInterfaces;
using DataAccess.Transport;
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
        var request = Transporter.UserMessageConverter(dto);
        var call = await _client.CreateUserAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("User wasn't created");
        }
    }

    public Task<User> GetUserByUsername(LoginDto loginDto)
    {
        var request = Transporter.UsernameMessageConverter(loginDto);
        var call = _client.UserByUsername(request);
        if (string.IsNullOrEmpty(call.Username))
        {
            throw new InvalidOperationException("No User found!");
        }

        var result = Transporter.UserConverter(call);
        return Task.FromResult(result);
    }

    public Task<List<ProjectDto>> GetProjects(string username)
    {
        var request = Transporter.UsernameMessageConverter(username);
        var projectsResponse = _client.GetAllProjects(request);
        /*if (projectsResponse.Projects.Count==0)
        {
            throw new InvalidOperationException("No projects were found");
        }*/

        var list = projectsResponse.Projects.Select(Transporter.ProjectDtoConverter).ToList();

        return Task.FromResult(list);
    }

    public Task<List<UserFinderDto>> LookForUsers(string username)
    {
        var request = Transporter.UsernameMessageConverter(username);
        var call = _client.LookForUsers(request);
        /*if (call.Users.Count == 0)
        {
            throw new InvalidOperationException("No users were found");
        }*/
        var list = call.Users.Select(Transporter.UserFinderDtoConverter).ToList();

        return Task.FromResult(list);
    }
}