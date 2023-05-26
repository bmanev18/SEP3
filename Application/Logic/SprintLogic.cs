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
    
    public async Task<Sprint> GetSprintById(int id)
    {
        return await _sprintDao.GetSprintById(id);
    }

    public async Task RemoveSprint(int id)
    {
        await _sprintDao.RemoveSprint(id);
    }

    public async Task AddUserStoryToSprint(UserStoryToSprintDto dto)
    {
        await _sprintDao.AddUserStoryToSprint(dto);
    }

    public async Task<List<UserStory>> GetUserStoriesFromSprint(int id)
        {
            return await _sprintDao.GetUserStoriesFromSprint(id);
        }

    public async Task<List<UserStory>> GetOtherUserStories(int id)
    {
        return await _sprintDao.GetOtherUserStories(id);
    }

    public async Task RemoveUserStoryFromSprint(UserStoryToSprintDto dto)
    {
        await _sprintDao.RemoveUserStoryFromSprint(dto);
    }
}