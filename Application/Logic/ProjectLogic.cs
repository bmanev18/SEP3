using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class ProjectLogic : IProjectLogic
{
    private readonly IProjectDao _projectDao;

    public ProjectLogic(IProjectDao projectDao)
    {
        _projectDao = projectDao;
    }


    public async Task CreateAsync(ProjectCreationDto dto)
    {
        await _projectDao.CreateAsync(dto);
    }

    public async Task AddCollaboratorAsync(AddUserToProjectDto collaborator)
    {
        var list = GetAllCollaboratorsAsync(collaborator.ProjectId);
        if (collaborator.Role.Equals("scrum master"))
        {
            Console.WriteLine("is master");
            var sm = list.Result.FirstOrDefault(u => u.Role.Equals("scrum master"));
            Console.WriteLine(sm==null);
            if (sm != null)
            {
                Console.WriteLine("exists");
                throw new InvalidOperationException("There is already a scrum master who is part of your project!");
            }
        }

        var dto = list.Result.FirstOrDefault(u => u.Username.Equals(collaborator.Username));
        if (dto != null) throw new InvalidOperationException("User is already a collaborator!");

        await _projectDao.AddCollaboratorAsync(collaborator);
    }

    public async Task<List<UserFinderDto>> GetAllCollaboratorsAsync(int projectId)
    {
        return await _projectDao.GetAllCollaboratorsAsync(projectId);
    }

    public async Task RemoveCollaboratorAsync(AddUserToProjectDto collaborator)
    {
        await _projectDao.RemoveCollaboratorAsync(collaborator);
    }

    public async Task AddUserStoryAsync(UserStoryCreationDto creationDto)
    {
        await _projectDao.AddUserStoryAsync(creationDto);
    }


    public async Task<List<UserStory>> GetUserStoriesAsync(int id)
    {
        return await _projectDao.GetUserStoriesAsync(id);
    }

    public async Task CreateSprintAsync(SprintCreationDto dto)
    {
        await _projectDao.CreateSprintAsync(dto);
    }

    public async Task<List<Sprint>> GetSprintsByProjectIdAsync(int projectId)
    {
        return await _projectDao.GetSprintsByProjectIdAsync(projectId);
    }

    public async Task CreateMeetingNoteAsync(Meeting meeting)
    {
        await _projectDao.CreateMeetingNoteAsync(meeting);
    }

    public async Task<List<Meeting>> GetMeetingNoteAsync(int projectId)
    {
        return await _projectDao.GetAllMeetingNotesAsync(projectId);
    }
}