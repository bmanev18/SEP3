/*using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Testing.MockDtos;

public class ProjectTestDao : IProjectDao
{
    private readonly List<ProjectDto> _projects;
    private int _projectId;

    private readonly Dictionary<int, List<string>> _collaboratorsInProject;

    private readonly List<UserStory> _userStories;
    private int _storyId;

    public ProjectTestDao()
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
            new() { ID = 1, Project_id = 1, Body = "Story 1", Priority = "low", Status = true, StoryPoints = 3 },
            new() { ID = 2, Project_id = 2, Body = "Story 2", Priority = "high", Status = false, StoryPoints = 5 },
            new() { ID = 3, Project_id = 3, Body = "Story 3", Priority = "critical", Status = true, StoryPoints = 8 },
            new() { ID = 4, Project_id = 1, Body = "Story 4", Priority = "high", Status = true, StoryPoints = 2 },
            new() { ID = 5, Project_id = 2, Body = "Story 5", Priority = "low", Status = false, StoryPoints = 1 }
        };
    }

    public Task CreateAsync(ProjectCreationDto dto)
    {
        var sizeBefore = _projects.Count;
        _projects.Add(new ProjectDto
        {
            Id = ++_projectId,
            Title = dto.Name
        });
        var sizeAfter = _projects.Count;

        AddCollaborator(new AddUserToProjectDto { ProjectID = _projectId, Username = dto.OwnerUsername });
        return Task.FromResult(sizeBefore != sizeAfter);
    }

    public Task<List<ProjectDto>> GetAllProjects(string username)
    {
        var keys = _collaboratorsInProject
            .Where(pair => pair.Value.Contains(username))
            .Select(pair => pair.Key)
            .ToList();
        return Task.FromResult(_projects.FindAll(dto => keys.Contains(dto.Id)));
    }

    public Task<int> AddCollaborator(AddUserToProjectDto dto)
    {
        if (_collaboratorsInProject.ContainsKey(dto.ProjectID))
        {
            _collaboratorsInProject[dto.ProjectID].Add(dto.Username);
        }
        else
        {
            _collaboratorsInProject.Add(dto.ProjectID, new List<string> { dto.Username });
        }

        return Task.FromResult(200);
    }

    public Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        var usernames = _collaboratorsInProject[id];
        var response = usernames.Select(username => new UserFinderDto { Username = username }).ToList();
        return Task.FromResult(response);
    }

    public Task<int> RemoveCollaborator(AddUserToProjectDto dto)
    {
        int code = 404;
        if (_collaboratorsInProject.ContainsKey(dto.ProjectID))
        {
            _collaboratorsInProject[dto.ProjectID].Remove(dto.Username);
            code = 200;
        }

        return Task.FromResult(code);
    }

    public Task<int> AddUserStory(UserStoryDto dto)
    {
        var sizeBefore = _userStories.Count;
        _userStories.Add(new UserStory
        {
            ID = ++_storyId,
            Project_id = dto.Project_id,
            Body = dto.Body,
            Priority = dto.Priority,
            Status = dto.Status,
            StoryPoints = dto.StoryPoints
        });
        var sizeAfter = _userStories.Count;
        return Task.FromResult(sizeBefore != sizeAfter ? 200 : 404);
    }

    public Task<List<UserStory>> GetUserStoriesAsync(int projectId)
    {
        return Task.FromResult(_userStories.FindAll(story => story.Project_id == projectId));
    }

    public Task UpdateUserStoryPointsAsync(int userStoryId, int points)
    {
        _userStories.First(story => story.ID == _storyId).StoryPoints = points;

        return Task.FromResult(_userStories.First(story => story.ID == _storyId).StoryPoints == points);
    }

    public Task DeleteUserStory(int id)
    {
        try
        {
            _userStories.Remove(_userStories.FirstOrDefault(story => story.ID == id) ??
                                throw new InvalidOperationException());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Task.CompletedTask;
    }
}*/