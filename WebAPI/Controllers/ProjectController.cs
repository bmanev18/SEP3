using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectLogic projectLogic;

    public ProjectController(IProjectLogic projectLogic)
    {
        this.projectLogic = projectLogic;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(ProjectCreationDto dto)
    {
        try
        {
            await projectLogic.CreateAsync(dto);
            return Created($"/project/{dto.Name}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}