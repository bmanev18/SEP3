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
        UserCreationDto user = new UserCreationDto("Andreea", "password","developer","Andreea", "Asimine");
        String actual = user.Username;
        String expected = "Andreea";
        Assert.AreEqual(expected, actual);
    }
    
    // [Test]
    // public void Test2()
    // {
    //     UserCreationDto user = new UserCreationDto("Andreea", "password","developer","Andreea", "Asimine");
    //     Assert.Throws<ArgumentException>(() => string.IsNullOrEmpty(user.Username));
    // }
    
    
}