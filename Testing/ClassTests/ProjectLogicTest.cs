using Application.Logic;
using Application.LogicInterfaces;
using NUnit.Framework;
using Shared.DTOs;
using Shared.Model;
using Testing.MockDAOs;

namespace Testing.ClassTests;

[TestFixture]
public class ProjectLogicTest
{
    private readonly IProjectLogic _logic = new ProjectLogic(new ProjectMockDao());

    [Test]
    public void Create_Successful()
    {
        var actual = _logic.CreateAsync(new ProjectCreationDto { Name = "New Project", OwnerUsername = "brambar" });
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }
    
    /*-------------------------------------------*/

    [Test]
    public void AddCollaborator_Successful()
    {
        var addUserToProjectDto = new AddUserToProjectDto { ProjectId = 1, Username = "user", Role = "developer" };
        var actual = _logic.AddCollaboratorAsync(addUserToProjectDto);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }
    
    [Test]
    public void AddCollaboratorSecondScrumMaster_Exception()
    {
        var addCollaborator = new AddUserToProjectDto { ProjectId = 1, Username = "jabbi", Role = "scrum master" };

        InvalidOperationException actual =Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await _logic.AddCollaboratorAsync(addCollaborator);
        })!;
        Assert.That(actual.Message, Is.EqualTo("There is already a scrum master who is part of your project!"));
    }
    
    [Test]
    public async Task AddCollaboratorExisting_Exception()
    {
        var addCollaborator = new AddUserToProjectDto { ProjectId = 1, Username = "johny", Role = "developer" };

        InvalidOperationException actual =Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await _logic.AddCollaboratorAsync(addCollaborator);
        })!;
        Assert.That(actual.Message, Is.EqualTo("User is already a collaborator!"));
    }

    /*-------------------------------------------*/
    
    [Test]
    public void GetAllCollaborators_Successful()
    {
        var actual = _logic.GetAllCollaboratorsAsync(3).Result;
        Assert.That(actual, Has.Count.EqualTo(1));
    }

    [Test]
    public void RemoveCollaborators_Successful()
    {
        var actual = _logic.RemoveCollaboratorAsync(new AddUserToProjectDto { ProjectId = 3, Username = "new" });
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void AddUserStory_Successful()
    {
        var actual = _logic.AddUserStoryAsync(new UserStoryCreationDto
            { ProjectId = 2, Body = "Test story", StoryPoints = 10, Priority = "low" });
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void GetUserStories_Successful()
    {
        var actual = _logic.GetUserStoriesAsync(1).Result;
        Assert.That(actual, Has.Count.EqualTo(2));
    }


    [Test]
    public void CreateSprint_Successful()
    {
        var created = new SprintCreationDto { ProjectId = 1, Name = "New sprint", StartDate = "23/5/2023", EndDate = "23/5/2023" };
        var actual = _logic.CreateSprintAsync(created);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }

    [Test]
    public void GetSprintsByProjectId()
    {
        var actual = _logic.GetSprintsByProjectIdAsync(2).Result;
        Assert.That(actual, Has.Count.EqualTo(2));
    }

    [Test]
    public void CreateMeetingNote_Successful()
    {
        var created = new Meeting { ProjectId = 1, Title = "Blh bla", Note = "bla bla", Author = "user2" };
        var actual = _logic.CreateMeetingNoteAsync(created);
        Assert.That(ReferenceEquals(actual, Task.CompletedTask));
    }
    
    [Test]
    public void GetMeetingNotes_Successful()
    {
        var actual = _logic.GetMeetingNoteAsync(1).Result;
        Assert.That(actual, Has.Count.EqualTo(2));
    }
}