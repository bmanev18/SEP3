namespace Shared.Model;

public class Project
{
    public int id { get; set; }
    public string Title { get; set; }
    public string ownerUsername { get; set; }
    public List<User> UsersOfProject { get; set; }
}