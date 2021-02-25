using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;

namespace AssignmentService.Profiles
{
    public class AssignmentsProfile : Profile
    {
        public AssignmentsProfile()
        {
            //Source -> Target
            CreateMap<Assignment, AssignmentDto>(); //Assignment read Dto
            CreateMap<AssignmentDto, Assignment>(); //Assignment create Dto
        }
    }
}