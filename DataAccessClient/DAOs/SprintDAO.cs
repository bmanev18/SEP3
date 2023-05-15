using Application.DAOInterfaces;
using DataAccessClient;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using UserStory = Shared.Model.UserStory;

namespace DataAccess.DAOs;

public class SprintDAO : ISprintDao
{
    
    private ProjectAccess.ProjectAccessClient client;
    
    
    public SprintDAO()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        client = new ProjectAccess.ProjectAccessClient(channel);
    }
   public Task CreateSprint(SprintCreationDto dto)
    {
        var request = new SprintCreationRequest();
        request.ProjectId = dto.Project_id;
        request.Name = dto.Name;
        request.StarDate = dto.StartDate;
        request.EndDate = dto.EndDate;
        client.CreateSprint(request);
        return Task.CompletedTask;
    }

    public async Task<Sprint> GetSprintById(int id)
    {
        var request = new Id();
        request.Id_ = id;
        var call =  client.GetSprintByID(request);
        Sprint sprint = new Sprint
        {
            Name = call.Name,
            Id = call.Id,
            StartDate = call.StarDate,
            EndDate = call.EndDate
        };
        return sprint;
    }

    public async Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
        var request = new Id();
        request.Id_ = id;
        var call = client.GetSprintByProjectId(request);
        List<Sprint> sprints = new List<Sprint>();
        foreach (var sprint in call.Sprints)
        {
            sprints.Add(new Sprint
            {
               Id = sprint.Id,
               Name = sprint.Name,
               StartDate = sprint.StarDate,
               EndDate = sprint.EndDate
            });
        }

        return sprints;
    }

    public Task RemoveSprint(SprintRemovalDto dto)
    {
        var request = new RemoveSprintMessage();
        request.ProjectId = dto.projectId;
        request.SprintId = dto.sprintId;
        client.RemoveSprint(request);
        return Task.CompletedTask;
    }

    public Task AddSprintTask(SprintTaskCreationDto task)
    {
        var request = new TaskRequest
        {
            Asignee = task.Assignee,
            Body = task.Body,
            Status = task.Status,
            StoryPoints = task.StoryPoint,
            StoryId = task.UserStoryId,
        };
        client.AddTask(request);
        return Task.CompletedTask;
    }

    public async Task<List<SprintTask>> GetTasks(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = client.GetTasks(request);
        List<SprintTask> tasks = new List<SprintTask>();
        foreach (var task in call.Tasks)
        {
            tasks.Add(new SprintTask
            {
                Assignee_username = task.Asignee,
                Body = task.Body,
                Id = task.Id,
                StoryPoint = task.StoryPoints,
                Status = task.Status
            });
        }

        return tasks;
    }
    
    public Task EditTask(SprintTask task)
    {
        TaskRequest request = new TaskRequest
        {
            Body = task.Body,
            Status = task.Status,
            StoryPoints = task.StoryPoint,
            Id = task.Id,
            Asignee = task.Assignee_username
        };
        client.EditTask(request);
        return Task.CompletedTask;
    }

    public Task AddUserStoryToSprint(UserStoryToSprintDto dto)
    {
        var request = new UserStoryToSprintRequest
        {
            SprintId = dto.SprintId,
            UserStoryId = dto.UserStoryId
        };
        client.AddUserStoryToSprint(request);
        return Task.CompletedTask;
    }

    public Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto)
    {
        var request = new UserStoryToSprintRequest
        {
            SprintId = dto.SprintId,
            UserStoryId = dto.UserStoryId
        };
        client.RemoveUserStoryFromSprint(request);
        return Task.CompletedTask;
    }

    public async Task<List<UserStory>> GetAllUserStoriesFromSprint(int id)
    {
        var request = new Id();
        request.Id_ = id;
        var call = client.GetAllUserStoriesFromSprint(request);
        List<UserStory> userStories = new List<UserStory>();
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
        var request = new Id();
        request.Id_ = id;
        client.RemoveTask(request);
    }

}