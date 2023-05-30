using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Testing.MockDAOs;

public class SprintMockDao : ISprintDao
{
    private readonly List<Sprint> _sprints;
    private readonly Dictionary<int, List<int>> _userStoriesInSprints;
    private readonly List<UserStory> _userStories;

    public SprintMockDao()
    {
        _sprints = new List<Sprint>
        {
            new() { Id = 1, ProjectId = 1, Name = "Sprint 1", StartDate = "23/5/2023", EndDate = "25/5/2023" },
            new() { Id = 2, ProjectId = 2, Name = "Sprint 2", StartDate = "25/5/2023", EndDate = "27/5/2023" },
            new() { Id = 3, ProjectId = 3, Name = "Sprint 1", StartDate = "17/6/2023", EndDate = "19/6/2023" },
            new() { Id = 4, ProjectId = 2, Name = "Sprint 2", StartDate = "23/7/2023", EndDate = "25/7/2023" },
            new() { Id = 5, ProjectId = 1, Name = "Sprint 2", StartDate = "18/4/2023", EndDate = "22/4/2023" }
        };

        _userStoriesInSprints = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 1, 4 } },
            { 2, new List<int> { 2, 5 } },
            { 3, new List<int> { 3 } }
        };

        _userStories = new List<UserStory>
        {
            new() { Id = 1, ProjectId = 1, Body = "Story 1", Priority = "low", Status = "done", StoryPoints = 3 },
            new() { Id = 2, ProjectId = 2, Body = "Story 2", Priority = "high", Status = "to-do", StoryPoints = 5 },
            new() { Id = 3, ProjectId = 3, Body = "Story 3", Priority = "critical", Status = "in progress", StoryPoints = 8 },
            new() { Id = 4, ProjectId = 1, Body = "Story 4", Priority = "high", Status = "done", StoryPoints = 2 },
            new() { Id = 5, ProjectId = 2, Body = "Story 5", Priority = "low", Status = "done", StoryPoints = 1 }
        };
    }

    public Task<Sprint> GetSprintByIdAsync(int id)
    {
        var first = _sprints.FirstOrDefault(sprint => sprint.Id == id);
        if (first == null) throw new NullReferenceException("Task not found");

        return Task.FromResult(first);
    }

    public Task RemoveSprintAsync(int id)
    {
        _sprints.RemoveAll(sprint => sprint.Id == id);
        return Task.CompletedTask;
    }

    public Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto)
    {
        var dtoSprintId = dto.SprintId;
        var dtoUserStoryId = dto.UserStoryId;
        if (_userStoriesInSprints.ContainsKey(dtoSprintId))
            _userStoriesInSprints[dtoSprintId].Add(dtoUserStoryId);
        else
            _userStoriesInSprints.Add(dtoSprintId, new List<int> { dtoUserStoryId });

        return Task.CompletedTask;
    }

    public Task<List<UserStory>> GetUserStoriesFromSprintAsync(int id)
    {
        var userStoriesIds = _userStoriesInSprints[id];
        var userStories = _userStories.FindAll(story => userStoriesIds.Contains(story.Id));
        return Task.FromResult(userStories);
    }

    public Task<List<UserStory>> GetOtherUserStoriesAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto)
    {
        _userStoriesInSprints[dto.SprintId].Remove(dto.UserStoryId);
        return Task.CompletedTask;
    }
}