using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;

namespace DataAccess.DAOs;

public class UserStoryDao : IUserStoryDao
{
    private readonly ProjectAccess.ProjectAccessClient _client;

    public UserStoryDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new ProjectAccess.ProjectAccessClient(channel);
    }

    public async Task UpdateUserStoryPointsAsync(int userStoryId, int points)
    {
        var request = new PointsUpdate
        {
            Id = userStoryId,
            Points = points
        };
        await _client.UpdateUserStoryPointsAsync(request);
    }
    public async Task UpdateUserStoryStatusAsync(int userStoryId, string status)
    {
        var request = new StatusUpdate()
        {
            Id = userStoryId,
            Status = status
        };
        await _client.UpdateUserStoryStatusAsync(request);
    }

    public async Task UpdateUserStoryPriorityAsync(int userStoryId, string priority)
    {
        var request = new PriorityUpdate()
        {
            Id = userStoryId,
            Priority = priority
        };
        await _client.UpdateUserStoryPriorityAsync(request);
    }

    public async Task DeleteUserStory(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        await _client.DeleteUserStoryAsync(request);
    }

    public Task AddSprintTask(SprintTaskCreationDto task)
    {
        {
            var request = new TaskCreationRequest
            {
                Assignee = task.Assignee,
                Body = task.Body,
                StoryPoints = task.StoryPoint,
                UserStoryId = task.UserStoryId
            };
            _client.AddTask(request);
            return Task.CompletedTask;
        }
    }

    public async Task EditTask(SprintTask task)
    {
        TaskRequest request = new TaskRequest
        {
            Id = task.Id,
            StoryId = task.UserStoryId,
            Assignee = task.Assignee,
            Body = task.Body,
            StoryPoints = task.StoryPoint,
            Status = task.Status
        };
        await _client.EditTaskAsync(request);
    }

    public Task<List<SprintTask>> GetTasks(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = _client.GetTasks(request);
        var tasks = new List<SprintTask>();
        foreach (var task in call.Tasks)
        {
            tasks.Add(new SprintTask
            {
                Id = task.Id,
                UserStoryId = task.StoryId,
                Assignee = task.Assignee,
                Body = task.Body,
                StoryPoint = task.StoryPoints,
                Status = task.Status
            });
        }

        return Task.FromResult(tasks);
    }

    public async Task DeleteTask(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        await _client.RemoveTaskAsync(request);
    }
}