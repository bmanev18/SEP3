using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IProjectService
{
    Task<Project> Create(ProjectCreationDto dto);


    Task AddCollaborator(int projectId, string username);

    Task<List<UserFinderDto>> GetAllCollaborators(int id);

    Task RemoveCollaborator(string username, int projectId);

    Task CreateUserStory(UserStoryDto dto);
    Task<IEnumerable<UserStory>> GetUserStoriesAsync(int? id = null);

    Task CreateSprint(SprintCreationDto dto, int id);

    Task<IEnumerable<Sprint>> GetSprintsAsync(int? projectId = null);
    Task CreateMeetingNote(Meeting meeting,  int id);
    Task<IEnumerable<Meeting>> GetMeetingNotesAsync(int projectId);
}