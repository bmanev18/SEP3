using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/{sprintId:int}")]
public class SprintController : ControllerBase
{
    private readonly ISprintLogic _sprintLogic;

    public SprintController(ISprintLogic sprintLogic)
    {
        _sprintLogic = sprintLogic;
    }

    [HttpGet]
    public async Task<ActionResult<Sprint>> GetSprintById(int sprintId)
    {
        try
        {
            var result = await _sprintLogic.GetSprintByIdAsync(sprintId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("")]
    public async Task<ActionResult> RemoveSprint([FromRoute] int sprintId)
    {
        try
        {
            await _sprintLogic.RemoveSprintAsync(sprintId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("userStory")]
    public async Task<ActionResult> AddUserStoryToSprint([FromRoute] int sprintId, [FromBody] int storyId)
    {
        try
        {
            var dto = new UserStoryToSprintDto
            {
                SprintId = sprintId,
                UserStoryId = storyId
            };
            await _sprintLogic.AddUserStoryToSprintAsync(dto);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("userStory")]
    public async Task<ActionResult<List<UserStory>>> GetAllUserStoriesFromSprint([FromRoute] int sprintId)
    {
        try
        {
            var list = await _sprintLogic.GetUserStoriesFromSprintAsync(sprintId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpDelete("userStory/{userStoryId:int}")]
    public async Task<ActionResult> RemoveUserStoryFromSprint([FromRoute] int sprintId, [FromRoute] int userStoryId)
    {
        var dto = new UserStoryToSprintDto
        {
            SprintId = sprintId,
            UserStoryId = userStoryId
        };
        try
        {
            await _sprintLogic.RemoveUserStoryFromSprintAsync(dto);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}