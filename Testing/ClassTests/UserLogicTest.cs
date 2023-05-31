using Application.DAOInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using DataAccessClient;
using NUnit.Framework;
using Testing.MockDAOs;
using UserCreationDto = Shared.DTOs.UserCreationDto;

namespace Testing.ClassTests;

[TestFixture]
public class UserLogicTest
{
    private readonly IUserLogic _logic = new UserLogic(new UserMockDao());

    [Test]
    public void GetProjects_Successful()
    {
        var actual = _logic.GetProjectsAsync("test").Result;
        Assert.That(actual, Has.Count.EqualTo(2));
    }

    [Test]
    public void LookForUsers_Successful()
    {
        var actual = _logic.LookForUsersAsync("user").Result;
        Assert.That(actual, Has.Count.EqualTo(5));
    }
}