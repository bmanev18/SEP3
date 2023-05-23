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
    private ProjectAccess.ProjectAccessClient _client;


    public SprintDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new ProjectAccess.ProjectAccessClient(channel);
    }

    public Task<Sprint> GetSprintById(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = _client.GetSprintByID(request);
        var sprint = new Sprint
        {
            Id = call.Id,
            ProjectId = call.ProjectId,
            Name = call.Name,
            StartDate = call.StarDate,
            EndDate = call.EndDate
        };
        return Task.FromResult(sprint);
    }

    public Task RemoveSprint(int id)
    {
        var request = new Id()
        {
            Id_ = id
        };
        _client.RemoveSprint(request);
        return Task.CompletedTask;
    }

    public Task AddUserStoryToSprint(UserStoryToSprintDto dto)
    {
        var request = new UserStoryToSprintRequest
        {
            SprintId = dto.SprintId,
            UserStoryId = dto.UserStoryId
        };
        _client.AddUserStoryToSprint(request);
        return Task.CompletedTask;
    }

    public Task<List<UserStory>> GetUserStoriesFromSprint(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = _client.GetAllUserStoriesFromSprint(request);
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
    }

    public Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto)
    {
        var request = new UserStoryToSprintRequest
        {
            SprintId = dto.SprintId,
            UserStoryId = dto.UserStoryId
        };
        _client.RemoveUserStoryFromSprint(request);
        return Task.CompletedTask;
    }
}