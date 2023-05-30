using Shared.DTOs;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IProjectService
{
    Task CreateAsync(ProjectCreationDto dto);


    Task AddCollaboratorAsync(int projectId, string username);

    Task<List<UserFinderDto>> GetAllCollaboratorsAsync(int id);

    Task RemoveCollaboratorAsync(string username, int projectId);

    Task CreateUserStoryAsync(UserStoryCreationDto creationDto);
    Task<IEnumerable<UserStory>> GetUserStoriesAsync(int? id = null);

    Task CreateSprintAsync(SprintCreationDto dto, int id);

    Task<IEnumerable<Sprint>> GetSprintsAsync(int? projectId = null);
    Task CreateMeetingNoteAsync(Meeting meeting, int id);
    Task<IEnumerable<Meeting>> GetMeetingNotesAsync(int projectId);
}