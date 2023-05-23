using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UserController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    [HttpGet("{username}/projects")]
    public async Task<ActionResult<List<ProjectDto>>> GetProjects(string username)
    {
        try
        {
            var list = await _userLogic.GetProjectsAsync(username);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    
    
    [HttpGet("search")]
    public async Task<ActionResult<List<UserFinderDto>>> LookForUsers([FromQuery]string username)
    {
        try
        {
            var list = await _userLogic.LookForUsersAsync(username);
            return Ok(list);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}