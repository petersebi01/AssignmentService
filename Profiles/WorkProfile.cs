using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;

namespace AssignmentService.Profiles
{
    public class WorkProfile : Profile
    {
        public WorkProfile()
        {
            //Source -> Target
            CreateMap<Work, WorkDto>();
            CreateMap<WorkDto, Work>();
        }
    }
}