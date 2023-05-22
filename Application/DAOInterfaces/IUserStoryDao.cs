using Shared.DTOs;

namespace Application.DAOInterfaces;

public interface IUserStoryDao
{
    Task CreateAsync(UserStoryDto dto);
}