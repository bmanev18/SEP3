// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Shared.DTOs;
using Shared.Model;

Console.WriteLine("Hello, World!");

//var info = { "id":1, "projectId":1, "name":"sprintTEST", "startDate":"2022-5-20", "endDate":"2022-5-30" };

var sprint = new Sprint()
{
    Id = 1,
    ProjectId = 1,
    Name = "Name",
     StartDate = "2022-5-20",
     EndDate = "2022-5-20"
};

var list = new List<Sprint>();
list.Add(sprint);

var serialize = JsonSerializer.Serialize(list,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

Console.WriteLine(serialize);

var deserialize = JsonSerializer.Deserialize<IEnumerable<Sprint>>(serialize,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    })!;

Console.WriteLine(deserialize.Count());