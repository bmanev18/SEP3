using Application.DAOInterfaces;
using DataAccessClient;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationDto = DataAccessClient.ProjectCreationDto;
using UserStory = Shared.Model.UserStory;

namespace DataAccess.DAOs;

public class ProjectDao : IProjectDao
{
    private ProjectAccess.ProjectAccessClient client;

    public ProjectDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        client = new ProjectAccess.ProjectAccessClient(channel);
    }


    public async Task CreateAsync(Shared.DTOs.ProjectCreationDto dto)
    {
        ProjectCreationDto request = new ProjectCreationDto
        {
            OwnerUsername = dto.OwnerUsername,
            Title = dto.Name
        };
        await client.CreateProjectAsync(request);
    }

    public async Task AddCollaborator(AddUserToProjectDto collaborator)
    {
        AddToProjectDto dto = new AddToProjectDto
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectID
        };
        await client.AddCollaboratorAsync(dto);
    }

    public Task<int> RemoveCollaborator(AddUserToProjectDto collaborator)
    {
        AddToProjectDto dto = new AddToProjectDto
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectID
        };
        var response = client.RemoveCollaborator(dto);
        return Task.FromResult(response.Code);
    }

    public Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        var collaboratorsResponse = client.GetAllCollaborators(new Id { Id_ = id });
        List<UserFinderDto> list = new List<UserFinderDto>();
        foreach (var user in collaboratorsResponse.Users)
        {
            list.Add(new UserFinderDto
                { FirstName = user.FirstName, LastName = user.LastName, Username = user.Username, Role = user.Role });
        }

        return Task.FromResult(list);
    }

    public async Task CreateSprint(SprintCreationDto dto, int id)
    {
        var request = new SprintCreationRequest
        {
            ProjectId = id,
            Name = dto.Name,
            StarDate = dto.StartDate,
            EndDate = dto.EndDate
        };
        await client.CreateSprintAsync(request);
    }

    public async Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
        var request = new Id
        {
            Id_ = id
        };
        var call = await client.GetSprintByProjectIdAsync(request);
        var sprints = new List<Sprint>();
        var callSprints = call.Sprints;
        foreach (var sprint in callSprints)
        {
            sprints.Add(new Sprint
            {
                Id = sprint.Id,
                ProjectId = sprint.ProjectId,
                Name = sprint.Name,
                StartDate = sprint.StarDate,
                EndDate = sprint.EndDate
            });
        }

        return sprints;
    }

    public Task<int> AddUserStory(UserStoryDto dto)
    {
        UserStoryMessage userStory = new UserStoryMessage
        {
            ProjectId = dto.Project_id,
            Priority = dto.Priority,
            TaskBody = dto.Body,
            StoryPoint = dto.StoryPoints
        };
        ResponseWithID responseWithId = client.AddUserStory(userStory);
        return Task.FromResult(responseWithId.Id);
    }

    public Task<List<ProjectDto>> GetAllProjects(string username)
    {
        var projectsResponse = client.GetAllProjects(new Username { Username_ = username });
        List<ProjectDto> list = new List<ProjectDto>();
        foreach (var project in projectsResponse.Projects)
        {
            list.Add(new ProjectDto() { Id = project.Id, Title = project.Title });
        }

        return Task.FromResult(list);
    }

    public Task<List<UserStory>> GetUserStoriesAsync(int id)
    {
        var productBacklog = client.GetUserStories(new Id() { Id_ = id });
        List<UserStory> list = new List<UserStory>();
        foreach (var story in productBacklog.UserStories)
        {
            list.Add(new UserStory
                { ID = story.Id, Body = story.UserStory_, Priority = story.Priority, Project_id = story.ProjectId });
        }

        return Task.FromResult(list);
    }
    public Task DeleteUserStory(int id)
    {
        throw new NotImplementedException();
    }
}