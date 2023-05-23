using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace HttpClients.Implementations;

public class ProjectHttpClient : IProjectService
{
    private readonly HttpClient client;

    public ProjectHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Project> Create(ProjectCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/project", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Project project = JsonSerializer.Deserialize<Project>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return project;
    }
    
    public async Task<IEnumerable<Project>> GetProjectsByUsername(string? username)
    {
        string uri = "/Project";
        if (!string.IsNullOrEmpty(username))
        {
            uri += $"/{username}";
        }
        Console.WriteLine(uri);

        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        IEnumerable<Project> projects = JsonSerializer.Deserialize<IEnumerable<Project>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return projects;
    }

    public async Task AddCollaborator(int projectId, string username)
    {
        var dto = new AddUserToProjectDto
        {
            ProjectID = projectId,
            Username = username,
        };
        var response = await client.PutAsJsonAsync($"project/{projectId}/collaborator", username);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
    }

    public async Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"project/{id}/collaborators");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response);
        }
        List<UserFinderDto> collaborators = JsonSerializer.Deserialize<List<UserFinderDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return collaborators;
    }

    public async Task RemoveCollaborator(string username, int projectid)
    {
        HttpResponseMessage response = await client.DeleteAsync($"/project/?username={username}&id={projectid}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    public async Task CreateUserStory(UserStoryDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/project/userStory", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }
    public async Task<IEnumerable<UserStory>> GetUserStoriesAsync(int? id)
    {
        var response = await client.GetAsync($"project/{id}/userStories");
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
    public async Task CreateSprint(SprintCreationDto dto,int id)
    {
        var response = await client.PostAsJsonAsync($"project/{id}/sprint", dto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }
    public async Task<IEnumerable<Sprint>> GetSprintsAsync(int? projectId)
    {
        var uri = projectId == null ? "/Sprint/getAllSprints" : $"/Sprint/getAllSprints/{projectId}";
        var response = await client.GetAsync(uri);
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

    
}