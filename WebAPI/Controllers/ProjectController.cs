using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationDto = Shared.DTOs.ProjectCreationDto;
using UserStory = Shared.Model.UserStory;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectLogic _projectLogic;

    public ProjectController(IProjectLogic projectLogic)
    {
        _projectLogic = projectLogic;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(ProjectCreationDto dto)
    {
        try
        {
            await _projectLogic.CreateAsync(dto);
            return Created($"/{dto.Name}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpPut("{id:int}/collaborator")]
    public async Task<ActionResult> AddCollaborator([FromRoute] int id, [FromBody] string username)
    {
       var collaborator = new AddUserToProjectDto
        {
            ProjectID= id,
            Username = username
        };
        try
        {
            await _projectLogic.AddCollaboratorAsync(collaborator);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}/collaborator")]
    public async Task<ActionResult<List<UserFinderDto>>> GetAllCollaborators([FromRoute] int id)
    {
        try
        {
            List<UserFinderDto> list = await _projectLogic.GetAllCollaborators(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}/collaborator/{username}")]
    public async Task<ActionResult> DeleteCollaborator([FromRoute]string username, [FromRoute]int id)
    {
        var dto = new AddUserToProjectDto
        {
            Username = username,
            ProjectID = id
        };
        try
        {
            await _projectLogic.RemoveCollaborator(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost("{id:int}/userStory")]
    public async Task<ActionResult> AddUserStory(UserStoryDto dto, int id)
    {
        try
        {
            await _projectLogic.AddUserStoryAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}/userStory")]
    public async Task<ActionResult<List<UserStory>>> GetUserStoriesAsync([FromRoute] int id)
    {
        try
        {
            List<UserStory> list = await _projectLogic.GetUserStoriesAsync(id);
            Console.WriteLine(list);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPost("{id:int}/sprint")] 
    public async Task<ActionResult> CreateSprint(SprintCreationDto dto, [FromRoute] int id)
    {
        try
        {
            await _projectLogic.CreateSprint(dto);
            return Created($"/{dto.Name}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id:int}/sprint")]
    public async Task<ActionResult<List<Sprint>>> GetSprintsByProjectId([FromRoute] int id)
    {
        try
        {
            var list = await _projectLogic.GetSprintsByProjectId(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}