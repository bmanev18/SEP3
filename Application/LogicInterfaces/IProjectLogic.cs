using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IProjectLogic
{
    Task CreateAsync(ProjectCreationDto dto);
    Task<Project>? GetByNameAsync(string name);
}