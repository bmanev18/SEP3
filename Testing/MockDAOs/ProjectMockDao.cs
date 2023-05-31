using System.Xml;
using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Testing.MockDAOs;

public class ProjectMockDao : IProjectDao

{
    private readonly List<Project> _projects;

    private readonly Dictionary<int, List<string>> _collaboratorsInProject;

    private readonly List<User> _users;

    private readonly List<UserStory> _userStories;

    private readonly List<Sprint> _sprints;

    private readonly List<Meeting> _meetings;


    public ProjectMockDao()
    {
        // Initial Data
        _projects = new List<Project>
        {
            new() { Id = 1, Title = "Project 1" },
            new() { Id = 2, Title = "Project 2" },
            new() { Id = 3, Title = "Project 3" }
        };

        _collaboratorsInProject = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "johny", "user" } },
            { 2, new List<string> { "jane", "mich", "emilybrown" } },
            { 3, new List<string> { "user" } }
        };

        _users = new List<User>
        {
            new()
                { Username = "johny", Password = "password1", FirstName = "John", LastName = "Doe", Role = "scrum master" },
            new()
                { Username = "janeSm", Password = "password2", FirstName = "Jane", LastName = "Smith", Role = "developer" },
            new()
                { Username = "mich", Password = "password3", FirstName = "Michael", LastName = "Johnson", Role = "product owner" },
            new()
                { Username = "jabbi", Password = "password4", FirstName = "Emily", LastName = "Brown", Role = "scrum master" },
            new()
                { Username = "user", Password = "password5", FirstName = "David", LastName = "Wilson", Role = "developer" }
        };

        _userStories = new List<UserStory>
        {
            new() { Id = 1, ProjectId = 1, Body = "Story 1", Priority = "low", Status = "done", StoryPoints = 3 },
            new() { Id = 2, ProjectId = 2, Body = "Story 2", Priority = "high", Status = "to-do", StoryPoints = 5 },
            new() { Id = 3, ProjectId = 3, Body = "Story 3", Priority = "critical", Status = "in progress", StoryPoints = 8 },
            new() { Id = 4, ProjectId = 1, Body = "Story 4", Priority = "high", Status = "done", StoryPoints = 2 },
            new() { Id = 5, ProjectId = 2, Body = "Story 5", Priority = "low", Status = "done", StoryPoints = 1 }
        };

        _sprints = new List<Sprint>
        {
            new() { Id = 1, ProjectId = 1, Name = "Sprint 1", StartDate = "23/5/2023", EndDate = "25/5/2023" },
            new() { Id = 2, ProjectId = 2, Name = "Sprint 2", StartDate = "25/5/2023", EndDate = "27/5/2023" },
            new() { Id = 3, ProjectId = 3, Name = "Sprint 1", StartDate = "17/6/2023", EndDate = "19/6/2023" },
            new() { Id = 4, ProjectId = 2, Name = "Sprint 2", StartDate = "23/7/2023", EndDate = "25/7/2023" },
            new() { Id = 5, ProjectId = 1, Name = "Sprint 2", StartDate = "18/4/2023", EndDate = "22/4/2023" }
        };

        _meetings = new List<Meeting>
        {
            new() { ProjectId = 1, Title = "Title1", Note = "New Note1", Author = "user1" },
            new() { ProjectId = 2, Title = "Title1", Note = "New Note1", Author = "user2" },
            new() { ProjectId = 1, Title = "Title2", Note = "New Note1", Author = "user1" },
            new() { ProjectId = 3, Title = "Title1", Note = "motto motto", Author = "user5" },
            new() { ProjectId = 4, Title = "Title1", Note = "New Note1", Author = "user4" }
        };
    }

    public Task CreateAsync(ProjectCreationDto dto)
    {
        var id = _projects.Count + 1;
        _projects.Add(new Project
        {
            Id = id,
            Title = dto.Name
        });

        AddCollaboratorAsync(new AddUserToProjectDto { ProjectId = id, Username = dto.OwnerUsername });
        return Task.CompletedTask;
    }


    public Task AddCollaboratorAsync(AddUserToProjectDto collaborator)
    {
        if (_collaboratorsInProject.ContainsKey(collaborator.ProjectId))
            _collaboratorsInProject[collaborator.ProjectId].Add(collaborator.Username);
        else
            _collaboratorsInProject.Add(collaborator.ProjectId, new List<string> { collaborator.Username });

        return Task.CompletedTask;
    }

    public Task<List<UserFinderDto>> GetAllCollaboratorsAsync(int id)
    {
        var usernames = _collaboratorsInProject[id];
        var users = _users.FindAll(user => usernames.Contains(user.Username));
        var response = users.Select(u => new UserFinderDto { Username = u.Username, FirstName = u.FirstName, LastName = u.LastName, Role = u.Role })
            .ToList();
        return Task.FromResult(response);
    }

    public Task RemoveCollaboratorAsync(AddUserToProjectDto dto)
    {
        if (!_collaboratorsInProject.ContainsKey(dto.ProjectId)) throw new InvalidOperationException("Unable to remove collaborator");

        _collaboratorsInProject[dto.ProjectId].Remove(dto.Username);

        return Task.CompletedTask;
    }

    public Task AddUserStoryAsync(UserStoryCreationDto creationDto)
    {
        _userStories.Add(new UserStory
        {
            Id = _userStories.Count + 1,
            ProjectId = creationDto.ProjectId,
            Body = creationDto.Body,
            Priority = creationDto.Priority,
            Status = "to-do",
            StoryPoints = creationDto.StoryPoints
        });
        return Task.CompletedTask;
    }

    public Task<List<UserStory>> GetUserStoriesAsync(int projectId)
    {
        return Task.FromResult(_userStories.FindAll(story => story.ProjectId == projectId));
    }

    public Task CreateSprintAsync(SprintCreationDto dto)
    {
        _sprints.Add(new Sprint()
        {
            Id = _sprints.Count + 1,
            ProjectId = dto.ProjectId,
            Name = dto.Name,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate!
        });
        return Task.CompletedTask;
    }

    public Task<List<Sprint>> GetSprintsByProjectIdAsync(int id)
    {
        return Task.FromResult(_sprints.FindAll(sprint => sprint.ProjectId == id));
    }

    public Task CreateMeetingNoteAsync(Meeting meeting)
    {
        _meetings.Add(meeting);
        return Task.CompletedTask;
    }

    public Task<List<Meeting>> GetAllMeetingNotesAsync(int id)
    {
        return Task.FromResult(_meetings.FindAll(meeting => meeting.ProjectId==id));
    }
}