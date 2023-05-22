using Application.Logic;
using Application.LogicInterfaces;
using NUnit.Framework;
using Shared.DTOs;
using Testing.MockDtos;

namespace Testing.ClassTests;

[TestFixture]
public class ProjectDaoTest
{
    private readonly IProjectLogic _logic = new ProjectLogic(new ProjectTestDao());


    

    [Test]
    public void GetAllProjects_Successful()
    {
        var expected = 2;
        var actual = _logic.GetAllProjects("test").Result.Count;
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void AddCollaborator_Successful()
    {
        var actual = _logic.AddCollaboratorAsync(new AddUserToProjectDto { ProjectID = 3, Username = "new" });
        Assert.That(actual.Id, Is.Not.EqualTo(null));
    }

    [Test]
    public void GetAllCollaborators_Successful()
    {
        var actual = _logic.GetAllCollaborators(1).Result;
        Assert.That(actual, Has.Count.EqualTo(2));
    }

    [Test]
    public void GRemoveCollaborators_Successful()
    {
        var actual = _logic.RemoveCollaborator(new AddUserToProjectDto { ProjectID = 3, Username = "new" }).Result;
        Assert.That(actual, Is.EqualTo(200));
    }
    
    [Test]
    public void AddUserStory_Successful()
    {
        var actual = _logic.AddUserStoryAsync(new UserStoryDto{Project_id = 2, Body = "Test story", StoryPoints = 10, Priority = "low", Status = true}).Result;
        Assert.That(actual, Is.EqualTo(200));
    }
    
    [Test]
    public void GetUserStories_Successful()
    {
        var actual = _logic.GetUserStoriesAsync(1).Result;
        Assert.That(actual, Has.Count.EqualTo(2));
    }
    
    [Test]
    public void UpdateUserStoryPoints_Successful()
    {
        var actual = _logic.UpdateUserStoryPointsAsync(6, 12);
        Assert.That(actual.Id,  Is.Not.EqualTo(null));
    }
    
    [Test]
    public void deleteUserStory_Successful()
    {
        var actual = _logic.DeleteUserStory(2);
        Assert.That(actual.Id,  Is.Not.EqualTo(null));
    }
    
    /*Task CreateAsync(ProjectCreationDto dto);
    + Task<List<ProjectDto>> GetAllProjects(string username);

    +Task AddCollaboratorAsync(AddUserToProjectDto collaborator);
    + Task<List<UserFinderDto>> GetAllCollaborators(int id);
    +Task<int> RemoveCollaborator(AddUserToProjectDto collaborator);
    
    +Task<int> AddUserStoryAsync(UserStoryDto dto);
    +Task<List<UserStory>> GetUserStoriesAsync(int id);
    +Task UpdateUserStoryPointsAsync(int userStoryId, int points);
    Task DeleteUserStory(int id);*/
}