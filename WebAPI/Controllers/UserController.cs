using Application.LogicInterfaces;
using DataAccessClient;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;
using UserCreationDto = Shared.DTOs.UserCreationDto;

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

    [HttpPost]
    public async Task<ActionResult> CreateAsync(UserCreationDto dto)
    {
        try
        {
           await _userLogic.CreateAsync(dto);
            return Created($"/user/{dto.Username}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<UserFinderDto>>> LookForUsers([FromQuery]string username)
    {
        try
        {
            var list = await _userLogic.LookForUsers(username);
            return Ok(list);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}