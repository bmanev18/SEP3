namespace HttpClients.Implementations;

using System.Net.Http.Json;
using System.Text.Json;
using ClientInterfaces;
using Shared.DTOs;
using Shared.Model;

public class SprintHttpClient : ISprintService
{
    private readonly HttpClient _client;

    public SprintHttpClient(HttpClient client)
    {
        _client = client;
    }
    public async Task RemoveStory(int sprintId, int storyId)
    {
        var response = await _client.DeleteAsync($"/sprint/{sprintId}/userStory/{storyId}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
    public async Task CreateTask(SprintTaskCreationDto dto)
    {
        var response = await _client.PostAsJsonAsync("/Sprint/AddSprintTask", dto);
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine("here" + result);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task UpdateTask(SprintTask task)
    {
        // TODO Controller missing method AssignSprintTask -> UpdateTask
        var response = await _client.PatchAsJsonAsync("/Sprint/assignSprintTask", task);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task RemoveTask(int taskId)
    {
        var response = await _client.DeleteAsync($"/Sprint/RemoveTask/{taskId}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
    
}