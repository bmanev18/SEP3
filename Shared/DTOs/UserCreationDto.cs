namespace Shared.DTOs;

public class UserCreationDto
{
    public string Username { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string Lastname { get; }
    
    public string Role { get; }

    public UserCreationDto(string username, string password, string role, string firstname, string lastname)
    {
        Username = username;
        Password = password;
        Role = role;
        FirstName = firstname;
        Lastname = lastname;
    }
    
}