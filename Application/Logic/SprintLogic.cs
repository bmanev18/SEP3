using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Model;

namespace Application.Logic;

public class SprintLogic : ISprintLogic
{
    private readonly ISprintDao _sprintDao;

    public SprintLogic(ISprintDao sprintDao)
    {
        _sprintDao = sprintDao;
    }

    public async Task<Sprint> GetSprintByIdAsync(int id)
    {
        return await _sprintDao.GetSprintByIdAsync(id);
    }

    public async Task RemoveSprintAsync(int id)
    {
        await _sprintDao.RemoveSprintAsync(id);
    }

    public async Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto)
    {
        await _sprintDao.AddUserStoryToSprintAsync(dto);
    }

    public async Task<List<UserStory>> GetUserStoriesFromSprintAsync(int id)
    {
        return await _sprintDao.GetUserStoriesFromSprintAsync(id);
    }

    public async Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto)
    {
        await _sprintDao.RemoveUserStoryFromSprintAsync(dto);
    }
}