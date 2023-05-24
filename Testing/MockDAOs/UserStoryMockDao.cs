using Application.DAOInterfaces;
using NUnit.Framework;
using Shared.DTOs;
using Shared.Model;

namespace Testing.MockDAOs;

public class UserStoryMockDao : IUserStoryDao
{
    private readonly List<UserStory> _userStories;
    private readonly List<SprintTask> _tasks;

    public UserStoryMockDao()
    {
        _userStories = new List<UserStory>
        {
            new() { ID = 1, Project_id = 1, Body = "Story 1", Priority = "high", Status = "to-Do", StoryPoints = 5 },
            new() { ID = 2, Project_id = 1, Body = "Story 2", Priority = "low", Status = "in progress", StoryPoints = 3 },
            new() { ID = 3, Project_id = 2, Body = "Story 3", Priority = "critical", Status = "to-Do", StoryPoints = 8 },
            new() { ID = 4, Project_id = 2, Body = "Story 4", Priority = "high", Status = "done", StoryPoints = 13 },
            new() { ID = 5, Project_id = 3, Body = "Story 5", Priority = "low", Status = "in progress", StoryPoints = 5 }
        };
        _tasks = new List<SprintTask>
        {
            new() { Id = 1, UserStoryId = 1, Assignee = "User1", Body = "Task 1", StoryPoint = 3, Status = "to-Do" },
            new() { Id = 2, UserStoryId = 2, Assignee = "User2", Body = "Task 2", StoryPoint = 5, Status = "in progress" },
            new() { Id = 3, UserStoryId = 3, Assignee = "User3", Body = "Task 3", StoryPoint = 2, Status = "done" },
            new() { Id = 4, UserStoryId = 4, Assignee = "User4", Body = "Task 4", StoryPoint = 8, Status = "to-Do" },
            new() { Id = 5, UserStoryId = 5, Assignee = "User5", Body = "Task 5", StoryPoint = 5, Status = "in progress" }
        };
    }

    public Task UpdateUserStoryPointsAsync(int id, int points)
    {
        _userStories.First(story => story.ID == id).StoryPoints = points;
        return Task.CompletedTask;
    }

    public Task UpdateUserStoryStatusAsync(int userStoryId, string status)
    {
        _userStories.First(story => story.ID == userStoryId).Status = status;
        return Task.CompletedTask;
    }

    public Task UpdateUserStoryPriorityAsync(int userStoryId, string priority)
    {
        _userStories.First(story => story.ID == userStoryId).Priority = priority;
        return Task.CompletedTask;
    }

    public Task DeleteUserStory(int id)
    {
        _userStories.RemoveAll(story => story.ID == id);

        return Task.CompletedTask;
    }

    public Task AddSprintTask(SprintTaskCreationDto dto)
    {
        _tasks.Add(new SprintTask
        {
            Id = _tasks.Count + 1,
            UserStoryId = dto.UserStoryId,
            Assignee = dto.Assignee,
            Body = dto.Body,
            StoryPoint = dto.StoryPoint,
            Status = "to-do"
        });
        return Task.CompletedTask;
    }

    public Task EditTask(SprintTask task)
    {
        var index = _tasks.FindIndex(t => t.Id == task.Id);
        _tasks[index] = task;
        return Task.CompletedTask;
    }

    public Task<List<SprintTask>> GetTasks(int id)
    {
        return Task.FromResult(_tasks.FindAll(task => task.UserStoryId == id));
    }

    public Task DeleteTask(int id)
    {
        _tasks.RemoveAll(task => task.Id == id);
        return Task.CompletedTask;
    }
}