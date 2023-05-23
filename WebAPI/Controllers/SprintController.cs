using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/{id:int}")]
public class SprintController : ControllerBase
{
    private readonly ISprintLogic _sprintLogic;

    public SprintController(ISprintLogic sprintLogic)
    {
        _sprintLogic = sprintLogic;
    }
    
    [HttpGet]
    public async Task<ActionResult<Sprint>> GetSprintById(int id)
    {
        try
        {
            var result = await _sprintLogic.GetSprintById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> RemoveSprint(int id)
    {
        try
        {
            await _sprintLogic.RemoveSprint(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("userStory")]
    public async Task<ActionResult> AddUserStoryToSprint(UserStoryToSprintDto dto)
    {
        try
        {
            await _sprintLogic.AddUserStoryToSprint(dto);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("userStory")]
    public async Task<ActionResult<List<UserStory>>> GetAllUserStoriesFromSprint(int id)
    {
        try
        {
            var list = await _sprintLogic.GetUserStoriesFromSprint(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpDelete("userStory/{userStoryId:int}")]
    public async Task<ActionResult> RemoveUserStoryFromSprint(int id, int userStoryId)
    {
        var dto = new UserStoryToSprintDto
        {
            SprintId = id,
            UserStoryId = userStoryId
        };
        try
        {
            await _sprintLogic.RemoveUserStoryFromSprint(dto);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}