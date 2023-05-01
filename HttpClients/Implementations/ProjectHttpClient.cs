using System.Net.Http.Json;
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
    
    public async Task<IEnumerable<Project>> GetProjects(string? nameContains = null)
    {
        string uri = "/projects";
        if (!string.IsNullOrEmpty(nameContains))
        {
            uri += $"?name={nameContains}";
        }
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
}