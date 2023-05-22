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
        this._sprintLogic = sprintLogic;
        /*
        Task CreateSprint(SprintCreationDto dto); // todo done
        Task<Sprint> GetSprintById(int id); //todo done
        Task<List<Sprint>> GetSprintsByProjectId(int id); //todo done
        Task RemoveSprint(int id); //todo done
        Task AddSprintTask(SprintTaskCreationDto dto); //todo done
        Task<List<SprintTask>> GetTasks(int id); //todo done
        Task AssignSprintTask(string username); //todo done
        Task AddUserStoryToSprint(UserStoryToSprintDto dto); //todo done
        Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto); //todo done
        Task<List<UserStory>> GetAllUserStoriesFromSprint(int id);*/ //todo done
    }
    
    //GET
    [HttpGet("{id:int}")]
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

    [HttpGet("{id:int}/userStories")]
    public async Task<ActionResult<List<UserStory>>> GetAllUserStoriesFromSprint(int id)
    {
        try
        {
            List<UserStory> list = await _sprintLogic.GetAllUserStoriesFromSprint(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    //PATCH
    [HttpPatch("assignSprintTask")]
    public async Task<ActionResult> AssignSprintTask(SprintTask task)
    {
        try
        {
            await _sprintLogic.EditTask(task);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("/userStory")]
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

    [HttpDelete("{id:int}/userStory/{userStoryId:int}")]
    public async Task<ActionResult> RemoveUserStoryFromSprint([FromQuery] int id, int userStoryId)
    {
        UserStoryToSprintDto dto = new UserStoryToSprintDto
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

    //DELETE
    [HttpDelete]
    public async Task<ActionResult> RemoveSprint(SprintRemovalDto dto)
    {
        try
        {
            await _sprintLogic.RemoveSprint(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
   
}