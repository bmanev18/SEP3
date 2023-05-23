using System.Net.Http.Json;
using System.Text.Json;
using Shared.DTOs;
using Shared.Model;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;

    public UserHttpClient(HttpClient client)
    {
        this._client = client;
    }

    public async Task<IEnumerable<Project>> GetProjectsByUsernameAsync(string? username)
    {
        var uri = username == null ? "/user" : $"/user/{username}/projects";

        var response = await _client.GetAsync(uri);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        var projects = JsonSerializer.Deserialize<IEnumerable<Project>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return projects;
    }

    public async Task<List<UserFinderDto>> LookForUsersAsync(string? usernameContains)
    {
        var response = await _client.GetAsync($"/user/search?username={usernameContains}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        var users = JsonSerializer.Deserialize<List<UserFinderDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
}