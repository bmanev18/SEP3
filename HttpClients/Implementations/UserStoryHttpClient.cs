using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace HttpClients.Implementations;

public class UserStoryHttpClient : IUserStoryService
{
    private readonly HttpClient client;

    public UserStoryHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
  

    public async Task<IEnumerable<SprintTask>> GetTasks(int id)
    {
        
        var response = await client.GetAsync($"UserStory/{id}/tasks");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        var sprintTasks = JsonSerializer.Deserialize<IEnumerable<SprintTask>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return sprintTasks;
    }

    public async Task CreateTask(SprintTaskCreationDto dto)
    {
        var response = await client.PostAsJsonAsync($"/UserStory/userStory/task",dto);
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine("here" + result);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task UpdateAsync(UserStoryUpdateDto dto)
    {
        // TODO!!!!
        var response = await client.PatchAsJsonAsync("/Project/UpdateStory", dto);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task RemoveAsync(int Id)
    {
        var response = await client.DeleteAsync($"userStory/{Id}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task UpdateStoryPointsAsync(int userStoryId, int points)
    {
        var response = await client.PatchAsJsonAsync("UserStory/storyPoints",points);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task UpdateUserStoryStatusAsync(int userStoryId, string status)
    {
        var uri = $"/UserStory/UpdateUserStoryStatus/{userStoryId}";
        var response = await client.PatchAsJsonAsync(uri, status);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task UpdateUserStoryPriorityAsync(int userStoryId, string priority)
    {
        var uri = $"/UserStory/UpdateUserStoryPriority/{userStoryId}";
        var response = await client.PatchAsJsonAsync(uri, priority);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
    public async Task RemoveTask(int taskId)
    {
        var response = await client.DeleteAsync($"/task/{taskId}");
        Console.WriteLine("12345678");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
    public async Task UpdateTask(SprintTask task)
    {
        var response = await client.PatchAsJsonAsync($"/UserStory/task", task);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }
    
}
