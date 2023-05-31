namespace Shared.Model;

public class User
{
    public required string Username { get; init; }
    public required string Password { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Role { get; init; }
}