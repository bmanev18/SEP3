using Application.DAOInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Testing.MockDAOs;

public class UserMockDao : IUserDao
{
    private readonly List<User> _users;
    private readonly List<Project> _projects;
    private readonly Dictionary<int, List<string>> _collaboratorsInProject;


    public UserMockDao()
    {
        _users = new List<User>
        {
            new()
                { Username = "user1", Password = "password1", Firstname = "John", Lastname = "Doe", Role = "scrum master" },
            new()
                { Username = "user2", Password = "password2", Firstname = "Jane", Lastname = "Smith", Role = "developer" },
            new()
                { Username = "user3", Password = "password3", Firstname = "Michael", Lastname = "Johnson", Role = "project owner" },
            new()
                { Username = "user4", Password = "password4", Firstname = "Emily", Lastname = "Brown", Role = "scrum master" },
            new()
                { Username = "user5", Password = "password5", Firstname = "David", Lastname = "Wilson", Role = "developer" }
        };

        _projects = new List<Project>
        {
            new() { Id = 1, Title = "Project 1" },
            new() { Id = 2, Title = "Project 2" },
            new() { Id = 3, Title = "Project 3" }
        };

        _collaboratorsInProject = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "test", "user" } },
            { 2, new List<string> { "random", "jabbi", "hello" } },
            { 3, new List<string> { "test" } }
        };
    }

    public Task CreateAsync(UserCreationDto dto)
    {
        List<string> roles = new() { "product owner", "scrum master", "developer" };
        if (!roles.Contains(dto.Role)) throw new ArgumentException("incorrect role");

        _users.Add(new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Firstname = dto.FirstName,
            Lastname = dto.Lastname,
            Role = dto.Role
        });
        return Task.CompletedTask;
    }

    public Task<User> GetUserByUsernameAsync(LoginDto loginDto)
    {
        var firstOrDefault = _users.FirstOrDefault(user =>
            user.Username.Equals(loginDto.Username) && user.Password.Equals(loginDto.Password));
        if (firstOrDefault == null) throw new NullReferenceException("No user was found");

        return Task.FromResult(firstOrDefault);
    }


    public Task<List<Project>> GetProjectsAsync(string username)
    {
        var keys = _collaboratorsInProject
            .Where(pair => pair.Value.Contains(username))
            .Select(pair => pair.Key)
            .ToList();

        var list = keys.Select(projectId => _projects.First(dto => dto.Id == projectId)).ToList();

        return Task.FromResult(list);
    }

    public Task<List<UserFinderDto>> LookForUsersAsync(string username)
    {
        var findAll = _users
            .FindAll(user => user.Username.Contains(username))
            .Select(user => new UserFinderDto
            {
                Username = user.Username,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Role = user.Role
            })
            .ToList();
        ;
        if (findAll == null) throw new NullReferenceException("No users were found");

        return Task.FromResult(findAll);
    }
}