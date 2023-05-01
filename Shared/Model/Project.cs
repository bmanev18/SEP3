namespace Shared.Model;

public class Project
{
    public string Title { get; set; }
    public string ownerUsername { get; set; }
    public List<User> UsersOfProject { get; set; }
}