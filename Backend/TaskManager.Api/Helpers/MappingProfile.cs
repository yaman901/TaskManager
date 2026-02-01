using AutoMapper;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserDTO>();

        // 2. NEW Map (DTO to User for the Update/Edit)
        // We ignore PasswordHash because UserDTO usually doesn't have it, 
        // and we don't want to overwrite the DB value with null.
        CreateMap<UserDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<CreateUserDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()).ReverseMap();

        // Project
        CreateMap<Project, ProjectDTO>()
            .ForMember(dest => dest.CreatedByUserName,
                opt => opt.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.Name : null));

        CreateMap<CreateProjectDTO, Project>();

        // TaskItem
        CreateMap<TaskItem, TaskItemDTO>()
            .ForMember(dest => dest.ProjectName,
                opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : null))
            .ForMember(dest => dest.AssignedUserName,
                opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.Name : null))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Priority,
                opt => opt.MapFrom(src => src.Priority.ToString()));

        CreateMap<CreateTaskItemDTO, TaskItem>()
    .ForMember(dest => dest.Status,
        opt => opt.MapFrom(src => ParseStatus(src.Status)))
    .ForMember(dest => dest.Priority,
        opt => opt.MapFrom(src => ParsePriority(src.Priority)));
    }
    private static TaskManager.Api.Models.TaskStatus ParseStatus(string status)
    {
        return Enum.TryParse<TaskManager.Api.Models.TaskStatus>(status, true, out var result)
            ? result
            : TaskManager.Api.Models.TaskStatus.InProgress;
    }

    private static TaskPriority ParsePriority(string priority)
    {
        return Enum.TryParse<TaskPriority>(priority, true, out var result)
            ? result
            : TaskPriority.Medium;
    }

}
