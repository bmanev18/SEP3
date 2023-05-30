using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using UserStory = Shared.Model.UserStory;
using DataAccess.Transport;

namespace DataAccess.DAOs;

public class SprintDao : ISprintDao
{
    private readonly SprintAccess.SprintAccessClient _client;


    public SprintDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new SprintAccess.SprintAccessClient(channel);
    }

    public async Task<Sprint> GetSprintByIdAsync(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var call = await _client.GetSprintByIDAsync(request);
        var sprint = Transporter.SprintConverter(call);
        return sprint;
    }

    public async Task RemoveSprintAsync(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var call = await _client.RemoveSprintAsync(request);
        if (!call.Response_) throw new InvalidOperationException("Unable to remove sprint");
    }

    public async Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto)
    {
        var request = Transporter.UserStorySprintRequestConverter(dto);
        var call = await _client.AddUserStoryToSprintAsync(request);
        if (!call.Response_) throw new InvalidOperationException("Unable to add user story to sprint");
    }

    public async Task<List<UserStory>> GetUserStoriesFromSprintAsync(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var call = await _client.GetAllUserStoriesFromSprintAsync(request);
        var callUserStories = call.UserStories;
        var userStories = callUserStories.Select(Transporter.UserStoryConverter)
            .ToList();

        return userStories;
    }

    public async Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto)
    {
        var request = Transporter.UserStorySprintRequestConverter(dto);
        var call = await _client.RemoveUserStoryFromSprintAsync(request);
        if (!call.Response_) throw new InvalidOperationException("Unable to remove user story from sprint");
    }
}