using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationDto = DataAccessClient.ProjectCreationDto;
using UserStory = Shared.Model.UserStory;

namespace DataAccess.DAOs;

public class ProjectDAO : IProjectDao
{
    private ProjectAccess.ProjectAccessClient client;

    public ProjectDAO()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        client = new ProjectAccess.ProjectAccessClient(channel);
    }


    public async Task CreateAsync(Shared.DTOs.ProjectCreationDto dto)
    {
        ProjectCreationDto request = new ProjectCreationDto
        {
            OwnerUsername = dto.ownerUsername,
            Title = dto.Name
        };
        var call = client.CreateProject(request);
    }

    public async Task AddCollaborator(AddUserToProjectDto collaborator)
    {
        AddToProjectDto dto = new AddToProjectDto
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectID
        };
        client.AddCollaborator(dto);
    }

    public  Task<int> AddUserStory(UserStoryDto dto)
    {
        UserStoryMessage userStory = new UserStoryMessage
        {
            ProjectId = dto.Project_id,
            Priority = dto.Priority,
            TaskBody = dto.Body
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
            list.Add(new ProjectDto(){Id = project.Id, Title = project.Title});
        }

        return Task.FromResult(list);
    }

    public Task<List<UserStory>> GetProductBacklog(int id)
    {
        var productBacklog = client.GetProductBacklog(new Id(){Id_ = id});
        List<UserStory> list = new List<UserStory>();
        foreach (var story in productBacklog.UserStories)
        {
            list.Add(new UserStory{ID = story.Id, Body = story.UserStory_, Priority = story.Priority, Project_id = story.ProjectId});
        }

        return Task.FromResult(list);
    }
}