using System.Net;
using System.Net.Http.Json;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class ProjectHttpClient : IProjectService
{
    private readonly HttpClient client;

    public ProjectHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    
    //todo un-comment
    /*public async Task CreateAsync(ProjectCreationDto dto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/project", dto);
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception();
        }
    }*/
    
}