using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpPost("{sprintId:int}/AdduserStory/{userStoryId:int}")]
    public async Task<ActionResult> AddUserStoryToSprint([FromRoute]int sprintId,[FromRoute]int userStoryId)
    {
        try
        {
            UserStoryToSprintDto dto = new UserStoryToSprintDto
            {
                SprintId = sprintId,
                UserStoryId = userStoryId
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

    [HttpGet("{id:int}/AllUserStories")]
    public async Task<ActionResult<List<UserStory>>> GetAllUserStoriesFromSprint([FromRoute]int id)
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
    
    
    [HttpGet("{id:int}/OtherUserStories")]
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
    }


    [HttpDelete("{id:int}/userStory/{userStoryId:int}")]
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