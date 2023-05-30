using DataAccessClient;
using Shared.DTOs;
using Shared.Model;
using ProjectDto = DataAccessClient.ProjectDto;

namespace DataAccess.Transport;

public static class Transporter
{
    public static ProjectCreationRequest ProjectCreationRequestConverter(ProjectCreationDto dto)
    {
        var request = new ProjectCreationRequest
        {
            OwnerUsername = dto.OwnerUsername,
            Title = dto.Name
        };
        return request;
    }

    public static UserProjectRequest UserProjectRequestConverter(AddUserToProjectDto collaborator)
    {
        var request = new UserProjectRequest
        {
            Username = collaborator.Username,
            ProjectId = collaborator.ProjectId
        };
        return request;
    }

    public static Id IdMessageConverter(int id)
    {
        var request = new Id { Id_ = id };
        return request;
    }

    public static MeetingNote MeetingNoteConverter(Meeting meeting)
    {
        Console.WriteLine($"webapi: {meeting.Title}");
        var request = new MeetingNote()
        {
            ProjectId = meeting.ProjectId,
            Author = meeting.Author,
            Note = meeting.Note,
            Title = meeting.Title
        };
        Console.WriteLine($"grpc: {request.Title}");
        return request;
    }

    public static Meeting MeetingConverter(MeetingNote note)
    {
        var request = new Meeting()
        {
            ProjectId = note.ProjectId,
            Author = note.Author,
            Note = note.Note,
            Title = note.Title
        };
        return request;
    }

    public static UserStoryCreationRequest UserStoryCreationRequestConverter(UserStoryCreationDto creationDto)
    {
        var request = new UserStoryCreationRequest()
        {
            ProjectId = creationDto.ProjectId,
            TaskBody = creationDto.Body,
            Priority = creationDto.Priority,
            StoryPoint = creationDto.StoryPoints
        };
        return request;
    }

    public static SprintCreationRequest SprintCreationRequestConverter(SprintCreationDto dto)
    {
        var request = new SprintCreationRequest
        {
            ProjectId = dto.ProjectId,
            Name = dto.Name,
            StarDate = dto.StartDate,
            EndDate = dto.EndDate
        };
        return request;
    }

    public static UserStory UserStoryConverter(UserStoryMessage story)
    {
        var userStory = new UserStory
        {
            Id = story.Id,
            Body = story.UserStory,
            Priority = story.Priority,
            ProjectId = story.ProjectId,
            StoryPoints = story.StoryPoint,
            Status = story.Status
        };
        return userStory;
    }

    public static Sprint SprintConverter(SprintMessage sprint)
    {
        var request = new Sprint
        {
            Id = sprint.Id,
            ProjectId = sprint.ProjectId,
            Name = sprint.Name,
            StartDate = sprint.StarDate,
            EndDate = sprint.EndDate
        };
        return request;
    }

    public static UserFinderDto UserFinderDtoConverter(UserSearchDto user)
    {
        var request = new UserFinderDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Role = user.Role
        };
        return request;
    }

    public static UserStorySprintRequest UserStorySprintRequestConverter(UserStoryToSprintDto dto)
    {
        var request = new UserStorySprintRequest
        {
            SprintId = dto.SprintId,
            UserStoryId = dto.UserStoryId
        };
        return request;
    }

    public static UserMessage UserMessageConverter(UserCreationDto dto)
    {
        var request = new UserMessage
        {
            Password = dto.Password,
            Username = dto.Username,
            Role = dto.Role,
            FirstName = dto.FirstName,
            LastName = dto.Lastname
        };
        return request;
    }

    public static Username UsernameMessageConverter(LoginDto loginDto)
    {
        var request = new Username
        {
            Username_ = loginDto.Username
        };
        return request;
    }

    public static Username UsernameMessageConverter(string username)
    {
        var request = new Username
        {
            Username_ = username
        };
        return request;
    }

    public static User UserConverter(UserMessage message)
    {
        var result = new User
        {
            Username = message.Username,
            Firstname = message.Username,
            Lastname = message.LastName,
            Password = message.Password,
            Role = message.Role
        };
        return result;
    }

    public static Project ProjectDtoConverter(ProjectDto project)
    {
        var request = new Project { Id = project.Id, Title = project.Title };
        return request;
    }

    public static PointsUpdate PointsUpdateConverter(int userStoryId, int points)
    {
        var request = new PointsUpdate
        {
            Id = userStoryId,
            Points = points
        };
        return request;
    }

    public static StatusUpdate StatusUpdateConverter(int userStoryId, string status)
    {
        var request = new StatusUpdate
        {
            Id = userStoryId,
            Status = status
        };
        return request;
    }

    public static PriorityUpdate PriorityUpdateConverter(int userStoryId, string priority)
    {
        var request = new PriorityUpdate
        {
            Id = userStoryId,
            Priority = priority
        };
        return request;
    }

    public static TaskCreationRequest TaskCreationRequestConverter(SprintTaskCreationDto task)
    {
        var request = new TaskCreationRequest
        {
            Assignee = task.Assignee,
            Body = task.Body,
            StoryPoints = task.StoryPoint,
            UserStoryId = task.UserStoryId
        };
        return request;
    }

    public static TaskMessage TaskMessageConverter(SprintTask task)
    {
        var request = new TaskMessage
        {
            Id = task.Id,
            StoryId = task.UserStoryId,
            Assignee = task.Assignee,
            Body = task.Body,
            StoryPoints = task.StoryPoint,
            Status = task.Status
        };
        return request;
    }

    public static SprintTask SprintTaskConverter(TaskMessage task)
    {
        var request = new SprintTask
        {
            Id = task.Id,
            UserStoryId = task.StoryId,
            Assignee = task.Assignee,
            Body = task.Body,
            StoryPoint = task.StoryPoints,
            Status = task.Status
        };
        return request;
    }
}