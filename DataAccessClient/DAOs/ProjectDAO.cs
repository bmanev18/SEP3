﻿using Application.DAOInterfaces;
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
    private readonly ProjectAccess.ProjectAccessClient _client;

    public ProjectDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new ProjectAccess.ProjectAccessClient(channel);
    }


    public async Task CreateAsync(Shared.DTOs.ProjectCreationDto dto)
    {
        ProjectCreationDto request = new ProjectCreationDto
        {
            OwnerUsername = dto.ownerUsername,
            Title = dto.Name
        };
       _client.CreateProject(request);
    }

    public Task<int> AddCollaborator(AddUserToProjectDto collaborator)
    {
        AddToProjectDto dto = new AddToProjectDto
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectID
        };
        var response = _client.AddCollaborator(dto);
        return Task.FromResult(response.Code);
    }
    
    public Task<int> RemoveCollaborator(AddUserToProjectDto collaborator)
    {
        AddToProjectDto dto = new AddToProjectDto
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectID
        };
        var response = _client.RemoveCollaborator(dto);
        return Task.FromResult(response.Code);

    }
    
    public Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {   
        var collaboratorsResponse = _client.GetAllCollaborators(new Id { Id_ = id });
        List<UserFinderDto> list = new List<UserFinderDto>();
        foreach (var user in collaboratorsResponse.Users)
        {
            list.Add(new UserFinderDto{FirstName = user.FirstName, LastName =user.LastName, Username = user.Username, Role = user.Role});
        }

        return Task.FromResult(list);
    }

    public async Task DeleteUserStory(int id)
    {
        var request = new Id();
        request.Id_ = id;
        _client.DeleteUserStory(request);
    }

    public Task<int> AddUserStory(UserStoryDto dto)
    {
        UserStoryMessage userStory = new UserStoryMessage
        {
            TaskBody = dto.Body, 
            Status = dto.Status,
            StoryPoint = dto.StoryPoints
        };
        ResponseWithID responseWithId = _client.AddUserStory(userStory);
        return Task.FromResult(responseWithId.Id);
    }

    public Task<List<ProjectDto>> GetAllProjects(string username)
    {
        var projectsResponse = _client.GetAllProjects(new Username { Username_ = username });
        List<ProjectDto> list = new List<ProjectDto>();
        foreach (var project in projectsResponse.Projects)
        {
            list.Add(new ProjectDto(){Id = project.Id, Title = project.Title});
        }

        return Task.FromResult(list);
    }

    public Task<List<UserStory>> GetProductBacklog(int id)
    {
        var productBacklog = _client.GetUserStories(new Id{Id_ = id});
        List<UserStory> list = new List<UserStory>();
        foreach (var story in productBacklog.UserStories)
        {
            list.Add(new UserStory{ID = story.Id, Body = story.UserStory_, Priority = story.Priority, Project_id = story.ProjectId});
        }

        return Task.FromResult(list);
    }
}