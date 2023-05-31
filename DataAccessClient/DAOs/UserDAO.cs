using Application.DAOInterfaces;
using DataAccess.Transport;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using LoginDto = Shared.DTOs.LoginDto;

namespace DataAccess.DAOs;

public class UserDao : IUserDao
{
    private readonly UserAccess.UserAccessClient _client;

    public UserDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        _client = new UserAccess.UserAccessClient(channel);
    }


    public async Task CreateAsync(UserCreationDto dto)
    {
        var request = Transporter.UserMessageConverter(dto);
        var call = await _client.CreateUserAsync(request);
        if (!call.Response_) throw new InvalidOperationException("User wasn't created");
    }

    public async Task<User> GetUserByUsernameAsync(LoginDto loginDto)
    {
        var request = Transporter.UsernameMessageConverter(loginDto);
        var call = await _client.UserByUsernameAsync(request);
        if (string.IsNullOrEmpty(call.Username)) throw new InvalidOperationException("No User found!");

        var result = Transporter.UserConverter(call);
        return result;
    }

    public async Task<List<Project>> GetProjectsAsync(string username)
    {
        var request = Transporter.UsernameMessageConverter(username);
        var projectsResponse = await _client.GetAllProjectsAsync(request);
        var list = projectsResponse.Projects.Select(Transporter.ProjectDtoConverter).ToList();

        return list;
    }

    public async Task<List<UserFinderDto>> LookForUsersAsync(string username)
    {
        var request = Transporter.UsernameMessageConverter(username);
        var call = await _client.LookForUsersAsync(request);
        var list = call.Users.Select(Transporter.UserFinderDtoConverter).ToList();

        return list;
    }
}