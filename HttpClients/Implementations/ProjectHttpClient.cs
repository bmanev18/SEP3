using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace HttpClients.Implementations;

public class ProjectHttpClient : IProjectService
{
    private readonly HttpClient _client;

    public ProjectHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task CreateAsync(ProjectCreationDto dto)
    {
        var response = await _client.PostAsJsonAsync("/Project",dto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task AddCollaboratorAsync(int projectId, string username,string role)
    {
        var response = await _client.PutAsJsonAsync($"project/{projectId}/collaborator/{username}", role);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task<List<UserFinderDto>> GetAllCollaboratorsAsync(int id)
    {
        var response = await _client.GetAsync($"/project/{id}/collaborator/");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Unable to get collaborators");
        }
        var collaborators = JsonSerializer.Deserialize<List<UserFinderDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return collaborators;
    }

    public async Task RemoveCollaboratorAsync(string username, int projectId)
    {
        var response = await _client.DeleteAsync($"/project/{projectId}/collaborator/{username}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task CreateUserStoryAsync(UserStoryCreationDto creationDto)
    {
        var response = await _client.PostAsJsonAsync($"/project/{creationDto.ProjectId}/userStory", creationDto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task<IEnumerable<UserStory>> GetUserStoriesAsync(int? id)
    {
        var response = await _client.GetAsync($"/project/{id}/userStory");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);

        var userStories = JsonSerializer.Deserialize<IEnumerable<UserStory>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return userStories;
    }

    public async Task CreateSprintAsync(SprintCreationDto dto, int id)
    {
        var response = await _client.PostAsJsonAsync($"project/{id}/sprint", dto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task<IEnumerable<Sprint>> GetSprintsAsync(int? projectId)
    {
        var response = await _client.GetAsync($"/Project/{projectId}/sprint");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
        var sprints = JsonSerializer.Deserialize<IEnumerable<Sprint>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return sprints;
    }

    public async Task CreateMeetingNoteAsync(Meeting meeting, int id)
    {
        var response = await _client.PostAsJsonAsync($"project/{id}/createNote", meeting);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
    }

    public async Task<IEnumerable<Meeting>> GetMeetingNotesAsync(int projectId)
    {
        var response = await _client.GetAsync($"/Project/{projectId}/getNotes");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(result);
        var notes = JsonSerializer.Deserialize<IEnumerable<Meeting>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return notes;
    }
}