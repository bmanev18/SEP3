namespace Shared.DTOs;

public class TaskCreationDto
{
    public string Name { get; set; }
    public string Assignee { get; set; }

    public int Points { get; set; }

    public TaskCreationDto(string name, string assignee, int points)
    {
        Name = name;
        Assignee = assignee;
        Points = points;
    }
}