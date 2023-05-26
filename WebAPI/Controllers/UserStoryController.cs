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


    
    [HttpPatch("storyPoints")]
    public async Task<ActionResult> UpdateUserStoryPoints([FromRoute] int id, int points)
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
    [HttpPatch("UpdateUserStoryStatus/{userStoryId}")]
    public async Task<ActionResult> UpdateUserStatus([FromBody]string status,[FromRoute] int userStoryId)
    {
        try
        {
            await _userStoryLogic.UpdateUserStoryStatusAsync(userStoryId, status);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPatch("UpdateUserStoryPriority/{userStoryId}")]
    public async Task<ActionResult> UpdateUserStoryPriority([FromBody]string priority,[FromRoute] int userStoryId)
    {
        try
        {
            await _userStoryLogic.UpdateUserStoryPriorityAsync(userStoryId, priority);
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
    
    [HttpPost("task")]
    public async Task<ActionResult> AddTask([FromBody] SprintTaskCreationDto dto)
    {
        try
        {
            await _userStoryLogic.AddTask(dto);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("task")]
    public async Task<IActionResult> EditTask([FromBody] SprintTask task)
    {
        try
        {
            await _userStoryLogic.EditTask(task);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    
    [HttpGet("{id:int}/tasks")]
    public async Task<ActionResult<List<SprintTask>>> GetTasks([FromRoute]int id)
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
    [HttpDelete("/task/{taskId:int}")]
    public async Task<ActionResult> RemoveTask([FromRoute]int taskId)
    {
        try
        {
            await _userStoryLogic.RemoveTask(taskId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}