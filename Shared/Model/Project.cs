namespace Shared.Model;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ownerUsername { get; set; }
    public List<User> UsersOfProject { get; set; }
}