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

    public async Task CreateSprint(SprintCreationDto dto)
    {
        var response = await _client.PostAsJsonAsync("/Sprint/CreateSprint", dto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task<IEnumerable<Sprint>> GetSprintsAsync(int? projectId)
    {
        var uri = projectId == null ? "/Sprint/getAllSprints" : $"/Sprint/getAllSprints/{projectId}";
        var response = await _client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        /*var hope = $"[{result}]";*/
        var sprints = JsonSerializer.Deserialize<IEnumerable<Sprint>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return sprints;
        
    }

    public async Task RemoveStory(int sprintId, int storyId)
    {
        var response = await _client.DeleteAsync($"/RemoveUserStory/?sprintId={sprintId}&userStoryId={storyId}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task<IEnumerable<SprintTask>> GetTasks(int? storyId)
    {
        var uri = (storyId == null) ? $"/Sprint/getAllTasks" : $"/Sprint/getAllTasks/{storyId}";

        var response = await _client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        var userStories = JsonSerializer.Deserialize<IEnumerable<SprintTask>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return userStories;
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
        var response = await _client.PatchAsJsonAsync("/Sprint/UpdateTask", task);
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