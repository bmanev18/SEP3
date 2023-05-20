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

    [HttpPost]
    public async Task<ActionResult> CreateAsync(UserStoryDto dto)
    {
        try
        {
            await _userStoryLogic.CreateAsync(dto);
            return Created($"/project/{dto.Project_id}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}