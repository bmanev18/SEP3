namespace Shared.DTOs;

public class UserStoryUpdateDto
{
    public int Id { get; set; }
    public string Priority { get; set; }
    public int StoryPoints { get; set; }
    public bool Status { get; set; }
}