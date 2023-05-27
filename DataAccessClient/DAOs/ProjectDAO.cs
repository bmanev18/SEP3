using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationRequest = DataAccessClient.ProjectCreationRequest;
using UserStory = Shared.Model.UserStory;

namespace DataAccess.DAOs;

public class ProjectDao : IProjectDao
{
    private readonly ProjectAccess.ProjectAccessClient _client;

    public ProjectDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new ProjectAccess.ProjectAccessClient(channel);
    }


    public async Task CreateAsync(ProjectCreationDto dto)
    {
        var request = new ProjectCreationRequest
        {
            OwnerUsername = dto.OwnerUsername,
            Title = dto.Name
        };
        var call = await _client.CreateProjectAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to create project");
        }
    }

    public async Task AddCollaborator(AddUserToProjectDto collaborator)
    {
        var dto = new UserProjectRequest()
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectID
        };
        var call = await _client.AddCollaboratorAsync(dto);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to add collaborator");
        }
    }

    public async Task RemoveCollaborator(AddUserToProjectDto collaborator)
    {
        var dto = new UserProjectRequest()
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectID
        };
        var call = await _client.RemoveCollaboratorAsync(dto);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to remove collaborator");
        }
    }

    public async Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        var collaboratorsResponse = await _client.GetAllCollaboratorsAsync(new Id { Id_ = id });
        var callCollaborators = collaboratorsResponse.Users;
        /*if (callCollaborators.Count == 0)
        {
            throw new InvalidOperationException("No collaborators were found");
        }*/

        var list = callCollaborators.Select(user => new UserFinderDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Role = user.Role
        }).ToList();

        return list;
    }

    public async Task AddUserStoryAsync(UserStoryDto dto)
    {
        var userStory = new UserStoryCreationRequest()
        {
            ProjectId = dto.Project_id,
            TaskBody = dto.Body,
            Priority = dto.Priority,
            StoryPoint = dto.StoryPoints
        };
        var call = await _client.AddUserStoryAsync(userStory);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to create user story");
        }
    }


    public async Task<List<UserStory>> GetUserStoriesAsync(int id)
    {
        var userStories = await _client.GetUserStoriesAsync(new Id { Id_ = id });
        var callUserStories = userStories.UserStories;
        /*if (callUserStories.Count == 0)
        {
            throw new InvalidOperationException("Unable to get user stories");
        }*/

        var list = callUserStories.Select(story => new UserStory
            {
                ID = story.Id,
                Body = story.UserStory,
                Priority = story.Priority,
                Project_id = story.ProjectId,
                StoryPoints = story.StoryPoint,
                Status = story.Status
            })
            .ToList();

        return await Task.FromResult(list);
    }

    public async Task CreateSprint(SprintCreationDto dto)
    {
        var request = new SprintCreationRequest
        {
            ProjectId = dto.ProjectId,
            Name = dto.Name,
            StarDate = dto.StartDate,
            EndDate = dto.EndDate
        };
        var call = await _client.CreateSprintAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to create sprint");
        }
    }

    public async Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
        var call = await _client.GetSprintByProjectIdAsync(new Id { Id_ = id });
        var callSprints = call.Sprints;
        /*if (callSprints.Count == 0)
        {
            throw new InvalidOperationException("No sprints were found");
        }*/

        return callSprints.Select(sprint => new Sprint
            {
                Id = sprint.Id,
                ProjectId = sprint.ProjectId,
                Name = sprint.Name,
                StartDate = sprint.StarDate,
                EndDate = sprint.EndDate
            })
            .ToList();
    }
}