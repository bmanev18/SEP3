using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserStoryController : ControllerBase
{
    private readonly IUserStoryLogic _userStoryLogic;

    public UserStoryController(IUserStoryLogic userStoryLogic)
    {
        _userStoryLogic = userStoryLogic;
    }

    
    [HttpPatch("{id:int}/storyPoints")]
    public async Task<ActionResult> UpdateUserStoryPoints(int points,[FromRoute] int id)
    {
        try
        {
            await _userStoryLogic.UpdateUserStoryPointsAsync(id, points);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUserStory([FromRoute] int id)
    {
        try
        {
            await _userStoryLogic.DeleteUserStory(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id:int}/tasks")]
    public async Task<ActionResult<List<SprintTask>>> GetTasks([FromRoute] int id)
    {
        try
        {
            List<SprintTask> list = await _userStoryLogic.GetTasks(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpDelete("{id:int}/task")]
    public async Task<ActionResult> RemoveTask(int id)
    {
        try
        {
            await _userStoryLogic.RemoveTask(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    //PUT
    [HttpPost("AddSprintTask")]
    public async Task<ActionResult> AddSprintTask([FromBody] SprintTaskCreationDto dto)
    {
        try
        {
            await _userStoryLogic.AddSprintTask(dto);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}