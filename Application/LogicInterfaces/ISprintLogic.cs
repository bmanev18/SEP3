﻿using Shared.DTOs;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ISprintLogic
{
    Task<Sprint> GetSprintByIdAsync(int id);
    Task RemoveSprintAsync(int id);

    Task AddUserStoryToSprintAsync(UserStoryToSprintDto dto);
    Task<List<UserStory>> GetUserStoriesFromSprintAsync(int id);
    Task RemoveUserStoryFromSprintAsync(UserStoryToSprintDto dto);
}