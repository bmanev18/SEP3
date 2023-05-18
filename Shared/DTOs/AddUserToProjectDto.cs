using Shared.Model;

namespace Shared.DTOs;

public class AddUserToProjectDto
{
    public int ProjectID {get; set;}
    public string Username { get; set; }
    
    public string Role { get; set; }

    public List<UserFinderDto> Users { get; set; }
}