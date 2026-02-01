using AutoMapper;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDTO>();
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Project mappings
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.Name));
            CreateMap<CreateProjectDTO, Project>();

            // TaskItem mappings
            CreateMap<TaskItem, TaskItemDTO>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));

            CreateMap<CreateTaskItemDTO, TaskItem>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<TaskManager.Api.Models.TaskStatus>(src.Status)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriority>(src.Priority)));
        }
    }
}
