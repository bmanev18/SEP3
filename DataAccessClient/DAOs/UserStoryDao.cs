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

    public async Task<List<SprintTask>> GetTasks(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call =  await _client.GetTasksAsync(request);
        var tasks = new List<SprintTask>();
        foreach (var task in call.Tasks)
        {
            tasks.Add(new SprintTask
            {
                Assignee = task.Asignee,
                Body = task.Body,
                Id = task.Id,
                StoryPoint = task.StoryPoints,
            });
        }

        return tasks;
    }

    public async Task RemoveTask(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
       await _client.RemoveTaskAsync(request);
    }

    public Task AddSprintTask(SprintTaskCreationDto task)
    {
        {
            var request = new TaskRequest
            {
                Asignee = task.Assignee,
                Body = task.Body,
                StoryPoints = task.StoryPoint,
                StoryId = task.UserStoryId,
            };
            _client.AddTask(request);
            return Task.CompletedTask;
        }
    }

    public async Task DeleteUserStory(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        await _client.DeleteUserStoryAsync(request);
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
    
}