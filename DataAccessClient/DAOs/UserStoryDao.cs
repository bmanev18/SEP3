using Application.DAOInterfaces;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;

namespace DataAccess.DAOs;

public class UserStoryDao : IUserStoryDao
{
    private readonly ProjectAccess.ProjectAccessClient _client;

    public UserStoryDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new ProjectAccess.ProjectAccessClient(channel);
    }

    public Task CreateAsync(UserStoryDto dto)
    {
        
        throw new NotImplementedException();
    }
}