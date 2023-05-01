using Shared.Model;

namespace Shared.DTOs;

public class AddUserToProjectDto
{
    public int ProjectID {get; set;}
    public User user { get; set; }
}