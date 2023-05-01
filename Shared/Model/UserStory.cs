namespace Shared.Model;

public class UserStory
{
    public int ID { get; init; }
    public string Body { get; set; }
    public string Priority { get; set; }
    public int Project_id { get; set; }
}