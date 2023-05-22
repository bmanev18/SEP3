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

    public Task CreateSprint(SprintCreationDto dto)
    {
        var request = new SprintCreationRequest
        {
            ProjectId = dto.ProjectId,
            Name = dto.Name,
            StarDate = dto.StartDate,
            EndDate = dto.EndDate.ToString()
        };
        _client.CreateSprint(request);
        return Task.CompletedTask;
    }

    public async Task<Sprint> GetSprintById(int id)
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
        return sprint;
    }

    public async Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = await _client.GetSprintByProjectIdAsync(request);
        var list = new List<Sprint>();
        foreach (var sprint in call.Sprints) 
        {
            list.Add(new Sprint
            {
                Id = sprint.Id,
                ProjectId = sprint.ProjectId,
                Name = sprint.Name,
                EndDate = sprint.EndDate,
                StartDate = sprint.StarDate
            });
        }

        return list;
    }

    public Task RemoveSprint(SprintRemovalDto dto)
    {
        var request = new RemoveSprintMessage
        {
            ProjectId = dto.projectId,
            SprintId = dto.sprintId
        };
        _client.RemoveSprint(request);
        return Task.CompletedTask;
    }

    public Task AddSprintTask(SprintTaskCreationDto task)
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

    public async Task<List<SprintTask>> GetTasks(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call =  _client.GetTasks(request);
        var tasks = new List<SprintTask>();
        foreach (var task in call.Tasks)
        {
            tasks.Add(new SprintTask
            {
                Assignee = task.Asignee,
                Body = task.Body,
                Id = task.Id,
                StoryPoint = task.StoryPoints,
                // Status = task.Status
            });
        }

        return tasks;
    }

    public Task EditTask(SprintTask task)
    {
        TaskRequest request = new TaskRequest
        {
            Body = task.Body,
            // Status = task.Status,
            StoryPoints = task.StoryPoint,
            Id = task.Id,
            Asignee = task.Assignee
        };
        _client.EditTask(request);
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

    public async Task<List<UserStory>> GetAllUserStoriesFromSprint(int id)
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
                Body = us.UserStory_,
                Priority = us.Priority,
                Project_id = us.ProjectId,
                ID = us.Id
            });
        }

        return userStories;
    }

    /*public Task<int> AddUserStory(UserStoryDto dto)
    {
        UserStoryMessage userStory = new UserStoryMessage
        {
            ProjectId = dto.Project_id,
            Priority = dto.Priority,
            TaskBody = dto.Body
        };
        ResponseWithID responseWithId = client.AddUserStory(userStory);
        return Task.FromResult(responseWithId.Id);
    }*/
    public async Task RemoveTask(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        _client.RemoveTask(request);
    }
}