namespace Shared.DTOs;

public class UserCreationDto
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string Lastname { get; init; }
    public required string Role { get; init; }
}