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
                { Username = "johny", Password = "password1", FirstName = "John", LastName = "Doe", Role = "scrum master" },
            new()
                { Username = "janeSm", Password = "password2", FirstName = "Jane", LastName = "Smith", Role = "developer" },
            new()
                { Username = "mich", Password = "password3", FirstName = "Michael", LastName = "Johnson", Role = "product owner" },
            new()
                { Username = "emilybrown", Password = "password4", FirstName = "Emily", LastName = "Brown", Role = "scrum master" },
            new()
                { Username = "user", Password = "password5", FirstName = "David", LastName = "Wilson", Role = "developer" }
        };

        _projects = new List<Project>
        {
            new() { Id = 1, Title = "Project 1" },
            new() { Id = 2, Title = "Project 2" },
            new() { Id = 3, Title = "Project 3" }
        };

        _collaboratorsInProject = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "johny", "user" } },
            { 2, new List<string> { "jane", "mich", "emilybrown" } },
            { 3, new List<string> { "user" } }
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
            FirstName = dto.FirstName,
            LastName = dto.LastName,
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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            })
            .ToList();
        if (findAll == null) throw new NullReferenceException("No users were found");

        return Task.FromResult(findAll);
    }
}