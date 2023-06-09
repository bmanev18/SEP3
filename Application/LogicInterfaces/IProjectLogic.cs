﻿using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task CreateAsync(ProjectCreationDto dto);

    Task AddCollaboratorAsync(AddUserToProjectDto collaborator);
    Task<List<UserFinderDto>> GetAllCollaboratorsAsync(int projectId);
    Task RemoveCollaboratorAsync(AddUserToProjectDto collaborator);

    Task AddUserStoryAsync(UserStoryCreationDto creationDto);
    Task<List<UserStory>> GetUserStoriesAsync(int projectId);

    Task CreateSprintAsync(SprintCreationDto dto);
    Task<List<Sprint>> GetSprintsByProjectIdAsync(int projectId);

    Task CreateMeetingNoteAsync(Meeting meeting);
    Task<List<Meeting>> GetMeetingNoteAsync(int projectId);
}