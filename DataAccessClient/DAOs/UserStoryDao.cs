using System.Formats.Tar;
using Application.DAOInterfaces;
using DataAccess.Transport;
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
        var request = Transporter.PointsUpdateConverter(userStoryId, points);
        var call = await _client.UpdateUserStoryPointsAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to update user story points");
        }
    }

    public async Task UpdateUserStoryStatusAsync(int userStoryId, string status)
    {
        var request = Transporter.StatusUpdateConverter(userStoryId, status);
        var call = await _client.UpdateUserStoryStatusAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to update user story status");
        }
    }

    public async Task UpdateUserStoryPriorityAsync(int userStoryId, string priority)
    {
        var request = Transporter.PriorityUpdateConverter(userStoryId, priority);
        var call = await _client.UpdateUserStoryPriorityAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to update user story priority");
        }
    }

    public async Task DeleteUserStoryAsync(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var call = await _client.DeleteUserStoryAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to delete user story");
        }
    }

    public async Task AddSprintTaskAsync(SprintTaskCreationDto task)
    {
        var request = Transporter.TaskCreationRequestConverter(task);
        var call = await _client.AddTaskAsync(request);
            if (!call.Response_)
            {
                throw new InvalidOperationException("Unable to add task to sprint");
            }
    }

    public async Task EditTaskAsync(SprintTask task)
    {
        var request = Transporter.TaskMessageConverter(task);
        var call = await _client.EditTaskAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to edit task");
        }
    }

    public async Task<List<SprintTask>> GetTasksAsync(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var call = await _client.GetTasksAsync(request);
        var callTasks = call.Tasks;
        /*if (callTasks.Count == 0)
        {
            throw new InvalidOperationException("No tasks were found");
        }*/

        var tasks = callTasks.Select(Transporter.SprintTaskConverter)
            .ToList();
        return tasks;
    }

    public async Task DeleteTaskAsync(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var call = await _client.RemoveTaskAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to delete task");
        }
    }
}