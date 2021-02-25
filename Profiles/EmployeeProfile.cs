using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;

namespace AssignmentService.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            //Source -> Target
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}