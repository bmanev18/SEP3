using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/{storyId:int}")]
public class UserStoryController : ControllerBase
{
    private readonly IUserStoryLogic _userStoryLogic;

    public UserStoryController(IUserStoryLogic userStoryLogic)
    {
        _userStoryLogic = userStoryLogic;
    }


    [HttpPatch("storyPoints")]
    public async Task<ActionResult> UpdateUserStoryPoints([FromRoute] int storyId, [FromBody] int points)
    {
        try
        {
            await _userStoryLogic.UpdateUserStoryPointsAsync(storyId, points);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("status")]
    public async Task<ActionResult> UpdateUserStatus([FromBody] string status, [FromRoute] int storyId)
    {
        try
        {
            await _userStoryLogic.UpdateUserStoryStatusAsync(storyId, status);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("priority")]
    public async Task<ActionResult> UpdateUserStoryPriority([FromBody] string priority, [FromRoute] int storyId)
    {
        try
        {
            await _userStoryLogic.UpdateUserStoryPriorityAsync(storyId, priority);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("")]
    public async Task<ActionResult> DeleteUserStory([FromRoute] int storyId)
    {
        try
        {
            await _userStoryLogic.DeleteUserStoryAsync(storyId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("task")]
    public async Task<ActionResult> AddTask([FromBody] TaskCreationDto dto)
    {
        try
        {
            await _userStoryLogic.AddTaskAsync(dto);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("task")]
    public async Task<IActionResult> EditTask([FromBody] TaskClass taskClass)
    {
        try
        {
            await _userStoryLogic.EditTaskAsync(taskClass);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpGet("task")]
    public async Task<ActionResult<List<TaskClass>>> GetTasks([FromRoute] int storyId)
    {
        try
        {
            var list = await _userStoryLogic.GetTasksAsync(storyId);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("task/{taskId:int}")]
    public async Task<ActionResult> RemoveTask([FromRoute] int taskId)
    {
        try
        {
            await _userStoryLogic.RemoveTaskAsync(taskId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}