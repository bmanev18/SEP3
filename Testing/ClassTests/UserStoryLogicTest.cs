using Application.Logic;
using Application.LogicInterfaces;
using NUnit.Framework;
using Shared.DTOs;
using Shared.Model;
using Testing.MockDAOs;

namespace Testing.ClassTests;

[TestFixture]
public class UserStoryLogicTest
{
    private readonly IUserStoryLogic _logic = new UserStoryLogic(new UserStoryMockDao());

    [Test]
    public void UpdatePoints_Successful()
    {
        var actual = _logic.UpdateUserStoryPointsAsync(1, 13);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void UpdateStatus_Successful()
    {
        var actual = _logic.UpdateUserStoryStatusAsync(1, "done");
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void UpdatePriority_Successful()
    {
        var actual = _logic.UpdateUserStoryPriorityAsync(1, "low");
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }


    [Test]
    public void DeleteUserStory_Successful()
    {
        var actual = _logic.DeleteUserStoryAsync(5);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }


    [Test]
    public void AddTask_Successful()
    {
        var created = new TaskCreationDto
        {
            UserStoryId = 1,
            Body = "New task",
            Assignee = "test",
            StoryPoints = 13
        };

        var actual = _logic.AddTaskAsync(created);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }


    [Test]
    public void EditTask_Successful()
    {
        var edited = new TaskClass()
        {
            Id = 1,
            UserStoryId = 3,
            Body = "New task",
            Assignee = "test",
            Status = "done",
            StoryPoints = 13
        };
        var actual = _logic.EditTaskAsync(edited);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void GetTasks_Successful()
    {
        var actual = _logic.GetTasksAsync(1).Result;
        Assert.That(actual, Has.Count.EqualTo(1));
    }

    [Test]
    public void RemoveTask_Successful()
    {
        var removeTask = _logic.RemoveTaskAsync(1);
        Assert.That(ReferenceEquals(removeTask, Task.CompletedTask));
    }
}