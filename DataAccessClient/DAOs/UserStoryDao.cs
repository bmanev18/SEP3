using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;

namespace DataAccess.DAOs;

public class UserStoryDao : IUserStoryDao
{
    private readonly UserStoryAccess.UserStoryAccessClient _client;

    public UserStoryDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new UserStoryAccess.UserStoryAccessClient(channel);
    }

    public async Task UpdateUserStoryPointsAsync(int userStoryId, int points)
    {
        var request = new PointsUpdate
        {
            Id = userStoryId,
            Points = points
        };
        var call = await _client.UpdateUserStoryPointsAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to update user story points");
        }
    }

    public async Task UpdateUserStoryStatusAsync(int userStoryId, string status)
    {
        var request = new StatusUpdate()
        {
            Id = userStoryId,
            Status = status
        };
        var call = await _client.UpdateUserStoryStatusAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to update user story status");
        }
    }

    public async Task UpdateUserStoryPriorityAsync(int userStoryId, string priority)
    {
        var request = new PriorityUpdate()
        {
            Id = userStoryId,
            Priority = priority
        };
        var call = await _client.UpdateUserStoryPriorityAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to update user story priority");
        }
    }

    public async Task DeleteUserStoryAsync(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = await _client.DeleteUserStoryAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to delete user story");
        }
    }

    public async Task AddSprintTaskAsync(SprintTaskCreationDto task)
    {
        {
            var request = new TaskCreationRequest
            {
                Assignee = task.Assignee,
                Body = task.Body,
                StoryPoints = task.StoryPoint,
                UserStoryId = task.UserStoryId
            };
            var call = await _client.AddTaskAsync(request);
            if (!call.Response_)
            {
                throw new InvalidOperationException("Unable to add task to sprint");
            }
        }
    }

    public async Task EditTaskAsync(SprintTask task)
    {
        var request = new TaskMessage
        {
            Id = task.Id,
            StoryId = task.UserStoryId,
            Assignee = task.Assignee,
            Body = task.Body,
            StoryPoints = task.StoryPoint,
            Status = task.Status
        };
        var call = await _client.EditTaskAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to edit task");
        }
    }

    public async Task<List<SprintTask>> GetTasksAsync(int id)
    {
        var call = await _client.GetTasksAsync(new Id { Id_ = id });
        var callTasks = call.Tasks;
        /*if (callTasks.Count == 0)
        {
            throw new InvalidOperationException("No tasks were found");
        }*/

        var tasks = callTasks.Select(task => new SprintTask
            {
                Id = task.Id,
                UserStoryId = task.StoryId,
                Assignee = task.Assignee,
                Body = task.Body,
                StoryPoint = task.StoryPoints,
                Status = task.Status
            })
            .ToList();
        return tasks;
    }

    public async Task DeleteTaskAsync(int id)
    {
        var call = await _client.RemoveTaskAsync(new Id{Id_ = id});
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to delete task");
        }
    }
}