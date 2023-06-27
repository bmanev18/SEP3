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
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpPut("{projectId:int}/collaborator/{username}")]
    public async Task<ActionResult> AddCollaborator([FromRoute] int projectId, [FromRoute] string username,[FromBody]string role)
    {
        var collaborator = new AddUserToProjectDto
        {
            ProjectId = projectId,
            Username = username,
            Role = role
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

    [HttpGet("{projectId:int}/collaborator")]
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

    [HttpDelete("{projectId:int}/collaborator/{username}")]
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


    [HttpPost("{projectId:int}/userStory")]
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

    [HttpGet("{projectId:int}/userStory")]
    public async Task<ActionResult<List<UserStory>>> GetUserStoriesAsync([FromRoute] int projectId)
    {
        try
        {
            var list = await _projectLogic.GetUserStoriesAsync(projectId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("{projectId:int}/sprint")]
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

    [HttpGet("{projectId:int}/sprint")]
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

    [HttpGet("{projectId:int}/getNotes")]
    public async Task<ActionResult<List<Meeting>>> GetMeetingNotes([FromRoute] int projectId)
    {
        try
        {
            var list = await _projectLogic.GetMeetingNoteAsync(projectId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("{projectId:int}/createNote")]
    public async Task<ActionResult> CreateMeetingNoteAsync([FromBody] Meeting meeting, [FromRoute] int projectId)
    {
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