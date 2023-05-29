using Application.DAOInterfaces;
using DataAccess.Transport;
using DataAccessClient;
using Grpc.Net.Client;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationRequest = DataAccessClient.ProjectCreationRequest;
using UserStory = Shared.Model.UserStory;

namespace DataAccess.DAOs;

public class ProjectDao : IProjectDao
{
    private readonly ProjectAccess.ProjectAccessClient _client;

    public ProjectDao()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:9111");
        Console.WriteLine(channel.State);
        _client = new ProjectAccess.ProjectAccessClient(channel);
    }


    public async Task CreateAsync(ProjectCreationDto dto)
    {
        var request = Transporter.ProjectCreationRequestConverter(dto);
        var call = await _client.CreateProjectAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to create project");
        }
    }

    public async Task AddCollaborator(AddUserToProjectDto collaborator)
    {
        var dto = Transporter.UserProjectRequestConverter(collaborator);
        var call = await _client.AddCollaboratorAsync(dto);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to add collaborator");
        }
    }
    public async Task CreateMeetingNote(Meeting meeting)
    {
        var request = Transporter.MeetingNoteConverter(meeting);
        var call =  _client.CreateMeetingNote(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to create note");
        }
    }
    public async Task<List<Meeting>> GetAllMeetingNotes(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var meetingNotes = await _client.GetMeetingNotesAsync(request);
        var callMeetingNotes = meetingNotes.MeetingNotes;
        var list = callMeetingNotes.Select(Transporter.MeetingConverter).ToList();
        return list;
    }

    public async Task RemoveCollaborator(AddUserToProjectDto collaborator)
    {
        var dto = Transporter.UserProjectRequestConverter(collaborator);
        var call = await _client.RemoveCollaboratorAsync(dto);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to remove collaborator");
        }
    }

    public async Task<List<UserFinderDto>> GetAllCollaborators(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var collaboratorsResponse = await _client.GetAllCollaboratorsAsync(request);
        var callCollaborators = collaboratorsResponse.Users;
        /*if (callCollaborators.Count == 0)
        {
            throw new InvalidOperationException("No collaborators were found");
        }*/

        var list = callCollaborators.Select(Transporter.UserFinderDtoConverter).ToList();

        return list;
    }

    public async Task AddUserStoryAsync(UserStoryDto dto)
    {
        var request = Transporter.UserStoryCreationRequestConverter(dto);
        var call = await _client.AddUserStoryAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to create user story");
        }
    }


    public async Task<List<UserStory>> GetUserStoriesAsync(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var userStories = await _client.GetUserStoriesAsync(request);
        var callUserStories = userStories.UserStories;
        /*if (callUserStories.Count == 0)
        {
            throw new InvalidOperationException("Unable to get user stories");
        }*/

        var list = callUserStories.Select(Transporter.UserStoryConverter).ToList();

        return await Task.FromResult(list);
    }

    public async Task CreateSprint(SprintCreationDto dto)
    {
        var request = Transporter.SprintCreationRequestConverter(dto);
        var call = await _client.CreateSprintAsync(request);
        if (!call.Response_)
        {
            throw new InvalidOperationException("Unable to create sprint");
        }
    }

    public async Task<List<Sprint>> GetSprintsByProjectId(int id)
    {
        var request = Transporter.IdMessageConverter(id);
        var call = await _client.GetSprintByProjectIdAsync(request);
        var callSprints = call.Sprints;
        /*if (callSprints.Count == 0)
        {
            throw new InvalidOperationException("No sprints were found");
        }*/

        return callSprints.Select(Transporter.SprintConverter)
            .ToList();
    }
}