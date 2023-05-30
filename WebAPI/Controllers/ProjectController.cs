using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationDto = Shared.DTOs.ProjectCreationDto;
using UserStory = Shared.Model.UserStory;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/{projectId:int}")]
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
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpPut("collaborator")]
    public async Task<ActionResult> AddCollaborator([FromRoute] int projectId, [FromBody] string username)
    {
        var collaborator = new AddUserToProjectDto
        {
            ProjectId = projectId,
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

    [HttpGet("collaborator")]
    public async Task<ActionResult<List<UserFinderDto>>> GetAllCollaborators([FromRoute] int projectId)
    {
        try
        {
            var list = await _projectLogic.GetAllCollaboratorsAsync(projectId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("collaborator/{username}")]
    public async Task<ActionResult> DeleteCollaborator([FromRoute] string username, [FromRoute] int projectId)
    {
        var dto = new AddUserToProjectDto
        {
            Username = username,
            ProjectId = projectId
        };
        try
        {
            await _projectLogic.RemoveCollaboratorAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost("userStory")]
    public async Task<ActionResult> AddUserStory(UserStoryCreationDto creationDto, int projectId)
    {
        try
        {
            await _projectLogic.AddUserStoryAsync(creationDto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("userStory")]
    public async Task<ActionResult<List<UserStory>>> GetUserStoriesAsync([FromRoute] int projectId)
    {
        try
        {
            var list = await _projectLogic.GetUserStoriesAsync(projectId);
            Console.WriteLine(list);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("sprint")]
    public async Task<ActionResult> CreateSprint(SprintCreationDto dto, [FromRoute] int projectId)
    {
        try
        {
            await _projectLogic.CreateSprintAsync(dto);
            return Created($"/{dto.Name}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("sprint")]
    public async Task<ActionResult<List<Sprint>>> GetSprintsByProjectId([FromRoute] int projectId)
    {
        try
        {
            var list = await _projectLogic.GetSprintsByProjectIdAsync(projectId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("getNotes")]
    public async Task<ActionResult<List<Meeting>>> GetMeetingNotes([FromRoute] int projectId)
    {
        try
        {
            var list = await _projectLogic.Async(projectId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("createNote")]
    public async Task<ActionResult> CreateMeetingNoteAsync([FromBody] Meeting meeting, [FromRoute] int projectId)
    {
        Console.WriteLine(meeting.Title);
        meeting.ProjectId = projectId;
        try
        {
            await _projectLogic.CreateMeetingNoteAsync(meeting);
            return Created($"/{meeting.Title}", meeting);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}