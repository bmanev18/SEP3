namespace Shared.DTOs;

public class AddUserToProjectDto
{
    public required int ProjectId { get; init; }
    public required string Username { get; init; }
}