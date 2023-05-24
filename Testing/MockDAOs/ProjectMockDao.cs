using System.Xml;
using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Testing.MockDAOs;

public class ProjectMockDao : IProjectDao
{
    private readonly List<ProjectDto> _projects;

    private readonly Dictionary<int, List<string>> _collaboratorsInProject;

    private readonly List<UserStory> _userStories;

    private readonly List<Sprint> _sprints;


    public ProjectMockDao()
    {
        // Initial Data
        _projects = new List<ProjectDto>
        {
            new ProjectDto { Id = 1, Title = "Project 1" },
            new ProjectDto { Id = 2, Title = "Project 2" },
            new ProjectDto { Id = 3, Title = "Project 3" }
        };

        _collaboratorsInProject = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "test", "user" } },
            { 2, new List<string> { "random", "jabbi", "hello" } },
            { 3, new List<string> { "test" } }
        };

        _userStories = new List<UserStory>
        {
            new() { ID = 1, Project_id = 1, Body = "Story 1", Priority = "low", Status = "done", StoryPoints = 3 },
            new() { ID = 2, Project_id = 2, Body = "Story 2", Priority = "high", Status = "to-do", StoryPoints = 5 },
            new() { ID = 3, Project_id = 3, Body = "Story 3", Priority = "critical", Status = "in progress", StoryPoints = 8 },
            new() { ID = 4, Project_id = 1, Body = "Story 4", Priority = "high", Status = "done", StoryPoints = 2 },
            new() { ID = 5, Project_id = 2, Body = "Story 5", Priority = "low", Status = "done", StoryPoints = 1 }
        };

        _sprints = new List<Sprint>
        {
            new() { Id = 1, ProjectId = 1, Name = "Sprint 1", StartDate = "23/5/2023", EndDate = "25/5/2023" },
            new() { Id = 2, ProjectId = 2, Name = "Sprint 2", StartDate = "25/5/2023", EndDate = "27/5/2023" },
            new() { Id = 3, ProjectId = 3, Name = "Sprint 1", StartDate = "17/6/2023", EndDate = "19/6/2023" },
            new() { Id = 4, ProjectId = 2, Name = "Sprint 2", StartDate = "23/7/2023", EndDate = "25/7/2023" },
            new() { Id = 5, ProjectId = 1, Name = "Sprint 2", StartDate = "18/4/2023", EndDate = "22/4/2023" },
        };
    }

    public Task CreateAsync(ProjectCreationDto dto)
    {
        var id = _projects.Count + 1;
        _projects.Add(new ProjectDto
        {
            Id = id,
            Title = dto.Name
        });

        AddCollaborator(new AddUserToProjectDto { ProjectID = id, Username = dto.OwnerUsername });
        return Task.CompletedTask;
    }


    public Task AddCollaborator(AddUserToProjectDto collaborator)
    {
        if (_collaboratorsInProject.ContainsKey(collaborator.ProjectID))
        {
            _collaboratorsInProject[collaborator.ProjectID].Add(collaborator.Username);
        }
        else
        {
            _collaboratorsInProject.Add(collaborator.ProjectID, new List<string> { collaborator.Username });
        }

        return Task.CompletedTask;
    }

    public Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        var usernames = _collaboratorsInProject[id];
        var response = usernames.Select(username => new UserFinderDto { Username = username }).ToList();
        return Task.FromResult(response);
    }

    public Task RemoveCollaborator(AddUserToProjectDto dto)
    {
        if (!_collaboratorsInProject.ContainsKey(dto.ProjectID))
        {
            throw new InvalidOperationException("Unable to remove collaborator");
        }

        _collaboratorsInProject[dto.ProjectID].Remove(dto.Username);

        return Task.CompletedTask;
    }

    public Task AddUserStory(UserStoryDto dto)
    {
        _userStories.Add(new UserStory
        {
            ID = _userStories.Count + 1,
            Project_id = dto.Project_id,
            Body = dto.Body,
            Priority = dto.Priority,
            Status = dto.Status,
            StoryPoints = dto.StoryPoints
        });
        return Task.CompletedTask;
    }

    public Task<List<UserStory>> GetUserStoriesAsync(int projectId)
    {
        return Task.FromResult(_userStories.FindAll(story => story.Project_id == projectId));
    }

    public Task CreateSprint(SprintCreationDto dto)
    {
        _sprints.Add(new Sprint()
        {
            Id = _sprints.Count + 1,
            ProjectId = dto.ProjectId,
            Name = dto.Name,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        });
        return Task.CompletedTask;
    }

    public Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
        return Task.FromResult(_sprints.FindAll(sprint => sprint.ProjectId == id));
    }
}