using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace HttpClients.Implementations;

public class UserStoryHttpClient : IUserStoryService
{
    private readonly HttpClient _client;

    public UserStoryHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task UpdateStoryPointsAsync(int id, int points)
    {
        var response = await _client.PatchAsJsonAsync($"/userStory/{id}/storyPoints", points);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task UpdateUserStoryStatusAsync(int id, string status)
    {
        var uri = $"/userStory/{id}/status";
        var response = await _client.PatchAsJsonAsync(uri, status);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task UpdateUserStoryPriorityAsync(int id, string priority)
    {
        var uri = $"/userStory/{id}/priority";
        var response = await _client.PatchAsJsonAsync(uri, priority);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task RemoveAsync(int id)
    {
        var response = await _client.DeleteAsync($"userStory/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task CreateTaskAsync(SprintTaskCreationDto dto)
    {
        var response = await _client.PostAsJsonAsync($"userStory/{dto.UserStoryId}/task", dto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task UpdateTaskAsync(SprintTask? task)
    {
        if (task != null)
        {
            var response = await _client.PatchAsJsonAsync($"/userStory/{task.UserStoryId}/task", task);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new Exception(result);
        }
    }

    public async Task<IEnumerable<SprintTask>> GetTasksAsync(int id)
    {
        var response = await _client.GetAsync($"userStory/{id}/task");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var sprintTasks = JsonSerializer.Deserialize<IEnumerable<SprintTask>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return sprintTasks;
    }

    public async Task RemoveTaskAsync(int taskId, int storyId)
    {
        var response = await _client.DeleteAsync($"userStory/{storyId}/task/{taskId}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
}