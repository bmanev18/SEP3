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

    public async Task<IEnumerable<UserStory>> OtherUserStories(int projectid)
    {
        var response = await _client.GetAsync($"/Sprint/{projectid}/OtherUserStories");
        Console.WriteLine("hehehe");
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

    public async Task AddUserStory(int sprintid, int storyid)
    {
        var response = await _client.PostAsync($"/sprint/{sprintid}/AdduserStory/{storyid}", null);
        if (!response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }    
    }

    public async Task CreateTask(SprintTaskCreationDto dto)
    {
        var response = await _client.PostAsJsonAsync("/userStory/task", dto);
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine("here" + result);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task<IEnumerable<UserStory>> GetUserStoriesFromSprint(int sprintid)
    {
        var response = await _client.GetAsync($"/Sprint/{sprintid}/AllUserStories");
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
}