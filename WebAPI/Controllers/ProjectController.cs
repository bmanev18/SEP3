﻿using Application.LogicInterfaces;
using DataAccessClient;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Model;
using ProjectCreationDto = Shared.DTOs.ProjectCreationDto;
using UserStory = Shared.Model.UserStory;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectLogic projectLogic;

    public ProjectController(IProjectLogic projectLogic)
    {
        this.projectLogic = projectLogic;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(ProjectCreationDto dto)
    {
        try
        {
            await projectLogic.CreateAsync(dto);
            return Created($"/project/{dto.Name}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> AddCollaborator(AddUserToProjectDto collaborator)
    {
        try
        {
            await projectLogic.AddCollaboratorAsync(collaborator);
            return Accepted();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("userStory")]
    public async Task<ActionResult<int>> AddUserStory(UserStoryDto dto)
    {
        try
        {
            int id = await projectLogic.AddUserStoryAsync(dto);
            return Ok(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCollaborator(string username, int id)
    {
        AddUserToProjectDto dto = new AddUserToProjectDto
        {
            Username = username,
            ProjectID = id
        };
        try
        {
            await projectLogic.RemoveCollaborator(dto);
            return Ok();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("getCollaborators/{id:int}")]
    public async Task<ActionResult<List<UserFinderDto>>> GetAllCollaborators([FromRoute]int id)
    {
        try
        {
            List<UserFinderDto> list = await projectLogic.GetAllCollaborators(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<List<ProjectDto>>> GetAllProjects([FromRoute]string username)
    {
        try
        {
           List<ProjectDto> list = await projectLogic.GetAllProjects(username);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<List<UserStory>>> GetProductBacklog([FromRoute] int id)
    {
        try
        {
            List<UserStory> list = await projectLogic.GetProductBacklog(id);
            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}