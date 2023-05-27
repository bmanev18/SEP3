using Application.DAOInterfaces;
using DataAccessClient;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using UserStory = Shared.Model.UserStory;
using System.Globalization;

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
        var request = new Id
        {
            Id_ = id
        };
        var call = await _client.GetSprintByIDAsync(request);
        /*if (call.ProjectId == 0)
        {
            throw new InvalidOperationException("No sprint was found");
        }*/

        var sprint = new Sprint
        {
            Id = call.Id,
            ProjectId = call.ProjectId,
            Name = call.Name,
            StartDate = call.StarDate,
            EndDate = call.EndDate
        };
        return sprint;
    }

    public async Task RemoveSprintAsync(int id)
    {
        var call = await _client.RemoveSprintAsync(new Id { Id_ = id });
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to remove sprint");
        }
    }

    public async Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto)
    {
        var request = new UserStorySprintRequest
        {
            SprintId = dto.SprintId,
            UserStoryId = dto.UserStoryId
        };
        var call = await _client.AddUserStoryToSprintAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to add user story to sprint");
        }
    }

    public async Task<List<UserStory>> GetUserStoriesFromSprintAsync(int id)
    {
        var call = await _client.GetAllUserStoriesFromSprintAsync(new Id { Id_ = id });
        var callUserStories = call.UserStories;
        /*if (callUserStories.Count == 0)
        {
            throw new InvalidOperationException("Unable to get user stories from sprint");
        }
        */

        var userStories = callUserStories.Select(us => new UserStory
            {
                ID = us.Id,
                Project_id = us.ProjectId,
                Body = us.UserStory,
                Priority = us.Priority,
                Status = us.Status,
                StoryPoints = us.StoryPoint
            })
            .ToList();

        return userStories;
    }

    /*public async Task<List<UserStory>> GetOtherUserStoriesAsync(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = await _client.GetOtherUserStories(request);
        var userStories = new List<UserStory>();
        foreach (var us in call.UserStories)
        {
            userStories.Add(new UserStory
            {
                ID = us.Id,
                Project_id = us.ProjectId,
                Body = us.UserStory_,
                Priority = us.Priority,
                Status = us.Status,
                StoryPoints = us.StoryPoint
                
            });
        }

        return Task.FromResult(userStories);
    }*/

    public async Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto)
    {
        var request = new UserStorySprintRequest
        {
            SprintId = dto.SprintId,
            UserStoryId = dto.UserStoryId
        };
        var call = await _client.RemoveUserStoryFromSprintAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to remove user story from sprint");
        }
    }
}