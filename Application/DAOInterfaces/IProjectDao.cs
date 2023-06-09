﻿using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IProjectDao
{
    Task CreateAsync(ProjectCreationDto dto);

    Task AddCollaboratorAsync(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaboratorsAsync(int id);
    Task RemoveCollaboratorAsync(AddUserToProjectDto collaborator);


    Task AddUserStoryAsync(UserStoryCreationDto creationDto);
    Task<List<UserStory>> GetUserStoriesAsync(int projectId);


    Task CreateSprintAsync(SprintCreationDto dto);
    Task<List<Sprint>> GetSprintsByProjectIdAsync(int id);

    Task CreateMeetingNoteAsync(Meeting meeting);
    Task<List<Meeting>> GetAllMeetingNotesAsync(int id);
}