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
    
    public async Task RemoveTask(int taskId)
    {
        var response = await client.DeleteAsync($"/Sprint/RemoveTask/{taskId}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
    public async Task<IEnumerable<SprintTask>> GetTasks(int id)
    {
        
        var response = await client.GetAsync($"/{id}/tasks");
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
        var response = await client.PostAsJsonAsync($"/userStory/task",dto);
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
    
 
}