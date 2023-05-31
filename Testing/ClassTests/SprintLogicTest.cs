using Application.Logic;
using Application.LogicInterfaces;
using NUnit.Framework;
using Shared.DTOs;
using Testing.MockDAOs;

namespace Testing.ClassTests;

[TestFixture]
public class SprintLogicTest
{
    private readonly ISprintLogic _logic = new SprintLogic(new SprintMockDao());

    [Test]
    public void GetSprintById_Successful()
    {
        var actual = _logic.GetSprintByIdAsync(1);
        Assert.That(actual, Is.Not.EqualTo(null));
    }

    [Test]
    public void RemoveSprint_Successful()
    {
        var actual = _logic.RemoveSprintAsync(1);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void AddUserStoryToSprint_Succeddful()
    {
        var add = new UserStoryToSprintDto
        {
            SprintId = 2,
            UserStoryId = 3
        };
        var actual = _logic.AddUserStoryToSprintAsync(add);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void GetUserStoriesFromSprint_Successful()
    {
        var actual = _logic.GetUserStoriesFromSprintAsync(2).Result;
        Assert.That(actual, Is.Not.EqualTo(null));
    }

    [Test]
    public void RemoveUserStoryFromSprint_Successful()
    {
        var remove = new UserStoryToSprintDto
        {
            SprintId = 2,
            UserStoryId = 3
        };
        var actual = _logic.RemoveUserStoryFromSprintAsync(remove);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }
}