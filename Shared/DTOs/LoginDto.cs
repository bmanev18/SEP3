namespace Shared.DTOs;

public class LoginDto
{
    public required string Username { get; set; }
    public string Password { get; set; }
}

// public record LoginDto1(string Username, string Password);