using Application.DAOInterfaces;
using DataAccess.DAOs;
using DataAccess.Transport;
using HttpClients.Implementations;
using NUnit.Framework;
using Shared.DTOs;
using Shared.Model;

namespace Testing;

public class Tests
{
    private IProjectDao _projectDao;

    [SetUp]
    public void Setup()
    {
        _projectDao = new ProjectDao(); // Initialize your implementation of IProjectDao here
    }


    //UserTesting
    [Test]
    public void UserTest1()
    {
        //"Andreea", "password","developer","Andreea", "Asimine"
        var user = new UserCreationDto
        {
            FirstName = "Andreea",
            Lastname = "Asimine",
            Password = "password",
            Role = "developer",
            Username = "Andreea1"
        };
        var actual = user.Username;
        var expected = "Andreea1";
        Assert.AreEqual(expected, actual);
    }

    // [Test]
    // public void UserTest2()
    // {
    //     UserCreationDto user = new UserCreationDto
    //     {
    //         FirstName = "Andreea",
    //         Lastname = "Asimine",
    //         Password = "password",
    //         Role = "developer",
    //         Username = ""
    //     };
    //     Assert.Throws<System.ArgumentException>(() => string.IsNullOrEmpty(user.Username));
    // }


    //ProjectTesting
    [Test]
    public void ProjectTest1()
    {
        var project = new ProjectCreationDto
        {
            Name = "Project1",
            OwnerUsername = "Andreea"
        };
        var actual = project.OwnerUsername;
        var expected = "Andreea";
        Assert.AreEqual(expected, actual);
    }

    // [Test]
    // public async Task CreateAsync_ShouldCreateProject()
    // {
    //     // Arrange
    //     var dto = new ProjectCreationDto
    //     {
    //         Name = "Test Project",
    //         ownerUsername = "Andreea"
    //     };
    //
    //     // Act
    //     await _projectDao.CreateAsync(dto);
    //
    //     // Assert
    //     var createdProject = await _projectDao.GetByNameAsync(dto);
    //     Assert.IsNotNull(createdProject);
    //     Assert.AreEqual(dto.Name, createdProject.Name);
    //     Assert.AreEqual(dto.ownerUsername, createdProject.ownerUsername);
    // }
}