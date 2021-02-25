using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;

namespace AssignmentService.Profiles
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            //Source -> Target
            CreateMap<Task, TaskDto>(); //Task read Dto
            CreateMap<TaskDto, Task>(); //Task create Dto
        }
    }
}