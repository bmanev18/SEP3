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
            var result = await _sprintLogic.GetSprintById(sprintId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> RemoveSprint(int sprintId)
    {
        try
        {
            await _sprintLogic.RemoveSprint(sprintId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("userStory")]
    public async Task<ActionResult> AddUserStoryToSprint([FromRoute]int sprintId,[FromBody]int storyId)
    {
        try
        {
            UserStoryToSprintDto dto = new UserStoryToSprintDto
            {
                SprintId = sprintId,
                UserStoryId = storyId
            }; 
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
    public async Task<ActionResult<List<UserStory>>> GetAllUserStoriesFromSprint([FromRoute]int sprintId)
    {
        try
        {
            var list = await _sprintLogic.GetUserStoriesFromSprint(sprintId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpDelete("userStory/{userStoryId:int}")]
    public async Task<ActionResult> RemoveUserStoryFromSprint(int sprintId, int userStoryId)
    {
        var dto = new UserStoryToSprintDto
        {
            SprintId = sprintId,
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

    /*[HttpGet("{id:int}/OtherUserStories")]
    public async Task<ActionResult<List<UserStory>>> OtherUserStories([FromRoute]int id)
    {
        try
        {
            var list = await _sprintLogic.GetOtherUserStories(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }*/
}