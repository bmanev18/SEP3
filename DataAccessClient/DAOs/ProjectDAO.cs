using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationDto = DataAccessClient.ProjectDto;
namespace DataAccess.DAOs;

public class ProjectDAO : IProjectDao
{
    private ProjectAccess.ProjectAccessClient client;

    public ProjectDAO()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        client = new ProjectAccess.ProjectAccessClient(channel);
    }


    public async Task CreateAsync(Shared.DTOs.ProjectCreationDto dto)
    {
        ProjectDto request = new ProjectDto
        {
            OwnerUsername = dto.ownerUsername,
            Title = dto.Name
        };
        var call = client.CreateProject(request);
    }
    

    public async Task<Project?> GetByNameAsync(Shared.DTOs.ProjectCreationDto dto)
    {
        ProjectDto request = new ProjectDto
        {
            OwnerUsername = dto.ownerUsername,
            Title = dto.Name
        };
    
        var call = client;
        
        Project project = new Project
        {
            
        };
    
        return project;
    }

    public async Task AddScrumMasterAsync(Shared.DTOs.AddUserToProjectDto dto)
    {
        UserSearchDto userSearchDto = new UserSearchDto
        {
            FirstName = dto.user.Firstname,
            LastName = dto.user.Lastname,
            Username = dto.user.Username
        };

        AddToProjectDto request = new AddToProjectDto
        {
            User = userSearchDto,
            ProjectId = dto.ProjectID
        };

        client.AddScrumMasterAsync(request);

    }
    
    public async Task AddDeveloperAsync(Shared.DTOs.AddUserToProjectDto dto)
    {
        UserSearchDto userSearchDto = new UserSearchDto
        {
            FirstName = dto.user.Firstname,
            LastName = dto.user.Lastname,
            Username = dto.user.Username
        };

        AddToProjectDto request = new AddToProjectDto
        {
            User = userSearchDto,
            ProjectId = dto.ProjectID
        };

        client.AddDeveloperAsync(request);

    }
    
}