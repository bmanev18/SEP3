using HttpClients.Implementations;
using Shared.DTOs;
using Shared.Model;

namespace Testing;

public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Test1()
    {
        //"Andreea", "password","developer","Andreea", "Asimine"
        UserCreationDto user = new UserCreationDto
        {
            FirstName = "Andreea",
            Lastname = "Asimine",
            Password = "password",
            Role = "developer",
            Username = "Andreea1"
        };
        String actual = user.Username;
        String expected = "Andreea1";
        Assert.AreEqual(expected, actual);
    }
    
    // [Test]
    // public void Test2()
    // {
    //     UserCreationDto user = new UserCreationDto("Andreea", "password","developer","Andreea", "Asimine");
    //     Assert.Throws<ArgumentException>(() => string.IsNullOrEmpty(user.Username));
    // }
    
    
}