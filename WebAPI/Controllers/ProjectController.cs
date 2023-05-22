using Application.LogicInterfaces;
using DataAccessClient;
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
       AddUserToProjectDto collaborator = new AddUserToProjectDto
        {
            ProjectID= id,
            Username = username
        };
        try
        {
            await projectLogic.AddCollaboratorAsync(collaborator);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCollaborator(string username, int id)
    {
        AddUserToProjectDto dto = new AddUserToProjectDto
        {
            Username = username,
            ProjectID = id
        };
        try
        {
            await projectLogic.RemoveCollaborator(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id:int}/collaborators")]
    public async Task<ActionResult<List<UserFinderDto>>> GetAllCollaborators([FromRoute] int id)
    {
        try
        {
            List<UserFinderDto> list = await projectLogic.GetAllCollaborators(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpPost("userStory")]
    public async Task<ActionResult> AddUserStory(UserStoryDto dto)
    {
        try
        {
            await projectLogic.AddUserStoryAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}/userStories")]
    public async Task<ActionResult<List<UserStory>>> GetUserStoriesAsync([FromRoute] int id)
    {
        try
        {
            List<UserStory> list = await projectLogic.GetUserStoriesAsync(id);
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
            await projectLogic.CreateSprint(dto,id);
            return Created($"/{dto.Name}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id:int}/sprints")]
    public async Task<ActionResult<List<Sprint>>> GetSprintsByProjectId([FromRoute] int id)
    {
        try
        {
            var list = await projectLogic.GetSprintsByProjectId(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}