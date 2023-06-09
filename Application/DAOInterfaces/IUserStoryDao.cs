﻿using Shared.DTOs;
using Shared.Model;

namespace Application.DAOInterfaces;

public interface IUserStoryDao
{
    Task UpdateUserStoryPointsAsync(int id, int points);
    Task UpdateUserStoryStatusAsync(int userStoryId, string status);
    Task UpdateUserStoryPriorityAsync(int userStoryId, string priority);
    Task DeleteUserStoryAsync(int id);

    Task AddTaskAsync(TaskCreationDto dto);
    Task EditTaskAsync(TaskClass taskClass);
    Task<List<TaskClass>> GetTasksAsync(int id);
    Task DeleteTaskAsync(int id);
}