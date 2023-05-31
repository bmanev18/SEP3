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

    public async Task<Sprint> GetSprintByIdAsync(int sprintId)
    {
        return await _sprintDao.GetSprintByIdAsync(sprintId);
    }

    public async Task RemoveSprintAsync(int sprintId)
    {
        await _sprintDao.RemoveSprintAsync(sprintId);
    }

    public async Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto)
    {
        await _sprintDao.AddUserStoryToSprintAsync(dto);
    }

    public async Task<List<UserStory>> GetUserStoriesFromSprintAsync(int sprintId)
    {
        return await _sprintDao.GetUserStoriesFromSprintAsync(sprintId);
    }

    public async Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto)
    {
        await _sprintDao.RemoveUserStoryFromSprintAsync(dto);
    }
}