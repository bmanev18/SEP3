namespace Shared.Model;

public class Project
{
    public string Name { get; set; }
    public string ownerUsername { get; set; }
    public List<User> UsersOfProject { get; set; }
}