namespace Shared.Model;

public class User
{
    public required string Username { get; init; }
    public required string Password { get; init; }

    public required string Firstname { get; init; }

    public required string Lastname { get; init; }

    public required string Role { get; init; }
}