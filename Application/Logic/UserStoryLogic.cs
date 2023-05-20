using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;

namespace Application.Logic;

public class UserStoryLogic:IUserStoryLogic
{
    private readonly IUserStoryDao _storyDao;

    public UserStoryLogic(IUserStoryDao storyDao)
    {
        _storyDao = storyDao;
    }

    public async Task CreateAsync(UserStoryDto dto)
    {
        await _storyDao.CreateAsync(dto);
    }
}