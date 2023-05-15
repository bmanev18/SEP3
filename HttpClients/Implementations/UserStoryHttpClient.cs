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
    
    public async Task<int> createUserStory(UserStory dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Project/userStory", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        int userStory = JsonSerializer.Deserialize<int>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return userStory;
    }

    public async Task<IEnumerable<UserStory>> getUserStory(int? id)
    {
        string uri = "/Project";
        if (id != null)
        {
            uri += $"/{id}";
        }

        HttpResponseMessage response = await client.GetAsync($"Project/UserStories/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<UserStory> userStories = JsonSerializer.Deserialize<IEnumerable<UserStory>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return userStories;
    }
}