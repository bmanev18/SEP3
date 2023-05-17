namespace Shared.Model;

public class Sprint
{ public int Id { get; init; }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}