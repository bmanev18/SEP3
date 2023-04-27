using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class ProjectLogic : IProjectLogic
{
    private readonly IProjectDao projectDao;

    public ProjectLogic(IProjectDao projectDao)
    {
        this.projectDao = projectDao;
    }
    
    
    public async Task CreateAsync(ProjectCreationDto dto)
    {
        await projectDao.CreateAsync(dto);
    }

    public Task<Project>? GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}