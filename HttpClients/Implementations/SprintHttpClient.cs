namespace HttpClients.Implementations;

using System.Net.Http.Json;
using System.Text.Json;
using ClientInterfaces;
using Shared.Model;

public class SprintHttpClient : ISprintService
{
    private readonly HttpClient _client;

    public SprintHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Sprint> GetSprintByIdAsync(int sprintId)
    {
        var response = await _client.GetAsync($"/sprint/{sprintId}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var sprint = JsonSerializer.Deserialize<Sprint>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return sprint;
    }

    public async Task DeleteSprintAsync(int sprintId)
    {
        var response = await _client.DeleteAsync($"/sprint/{sprintId}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task AddUserStoryToSprintAsync(int sprintId, int storyId)
    {
        var response = await _client.PostAsJsonAsync($"/sprint/{sprintId}/userStory", storyId);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task<IEnumerable<UserStory>> GetUserStoriesFromSprintAsync(int sprintId)
    {
        var response = await _client.GetAsync($"/sprint/{sprintId}/userStory");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var userStories = JsonSerializer.Deserialize<IEnumerable<UserStory>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return userStories;
    }

    public async Task RemoveStoryFromSprintAsync(int sprintId, int storyId)
    {
        var response = await _client.DeleteAsync($"/sprint/{sprintId}/userStory/{storyId}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
}