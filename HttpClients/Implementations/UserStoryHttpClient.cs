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
        this._client = client;
    }

    public async Task<int> CreateUserStory(UserStoryDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Project/userStory", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        int userStory = JsonSerializer.Deserialize<int>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return userStory;
    }

    public async Task<IEnumerable<UserStory>> GetUserStoriesAsync(int? id)
    {
        var uri = (id == null) ? $"/Project/UserStories" : $"/Project/UserStories/{id}";
        var response = await _client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        var userStories = JsonSerializer.Deserialize<IEnumerable<UserStory>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return userStories;
    }

    public async Task UpdateAsync(UserStoryUpdateDto dto)
    {
        // TODO!!!!
        var response = await _client.PatchAsJsonAsync("/Project/UpdateStory", dto);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task RemoveAsync(int storyId)
    {
        var response = await _client.DeleteAsync($"/Project/DeleteStory/{storyId}");
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
}