using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IProjectDao
{
    Task CreateAsync(ProjectCreationDto dto);
    Task<Project?> GetByNameAsync(ProjectCreationDto dto);
    Task AddScrumMasterAsync(AddUserToProjectDto dto);
    Task AddDeveloperAsync(AddUserToProjectDto dto);

}