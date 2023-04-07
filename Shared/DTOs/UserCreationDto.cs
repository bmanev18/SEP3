namespace Shared.DTOs;

public class UserCreationDto
{
    public string Username { get; }
    public string Password { get; }

    public UserCreationDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
}