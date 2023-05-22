using Shared.DTOs;

namespace Application.LogicInterfaces;

public interface IUserStoryLogic
{
    Task CreateAsync(UserStoryDto dto);

}