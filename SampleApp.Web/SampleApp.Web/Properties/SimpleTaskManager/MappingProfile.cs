using AutoMapper;
using SimpleTaskManager.Core.Model;
using SimpleTaskManager.Dtos;
using SimpleTaskManager.ViewModels;

namespace SimpleTaskManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskViewModel, Assignment>();
            CreateMap<Assignment, EditTaskViewModel>();
            CreateMap<EditTaskViewModel, Assignment>();
            CreateMap<TaskViewModel, ValidateTaskRequestDto>();
        }
    }
}
