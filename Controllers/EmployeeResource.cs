using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssignmentService.Data;
using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AssignmentService.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeResource : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public EmployeeResource(IEmployeeRepository employeeEmployeeRepository, IMapper mapper, IConfiguration configuration)
        {
            _employeeRepository = employeeEmployeeRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> FindAllEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet("{id}", Name = "FindEmployeeById")]
        public ActionResult<EmployeeDto> FindEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee != null)
            {
                return Ok(_mapper.Map<EmployeeDto>(employee)); 
            }

            return NotFound();
        }

        // [AllowAnonymous]
        // [Route("/login")]
        // [HttpPost]
        // public IActionResult Login(EmployeeDto employeeDto)
        // {
        //     IActionResult response = Unauthorized();
        //     Employee userCredentials = _employeeRepository.GetLoginCredentials(employeeDto);
        //
        //     if (userCredentials != null)
        //     {
        //         var secret = _configuration["Jwt:secret"];
        //
        //         response = Ok(new
        //         {
        //             token = secret,
        //             userDetails = userCredentials
        //         });
        //     }
        //
        //     return response;
        // }

        //[Authorize(Policy = "Employee")]
        [HttpPost]
        public ActionResult<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            var employeeToCreate = _mapper.Map<Employee>(employeeDto);
            
            _employeeRepository.CreateEmployee(employeeToCreate);
            _employeeRepository.SaveChanges();
                
            var employeeCreated = _mapper.Map<EmployeeDto>(employeeToCreate);
            return CreatedAtRoute(nameof(FindEmployeeById), new {Id = employeeCreated.EmployeeId},
                employeeDto);
        }

        //[Authorize(Policy = "Employee")]
        [HttpPatch("{id}")]
        public ActionResult UpdateEmployee(int id, JsonPatchDocument<EmployeeDto> patchDocument)
        {
            // check if the updatable assignment exists
            var employeeModelFromRepo = _employeeRepository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }

            var employeeToPatch = _mapper.Map<EmployeeDto>(employeeModelFromRepo);
            patchDocument.ApplyTo(employeeToPatch, ModelState);

            if (!TryValidateModel(employeeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(employeeToPatch, employeeModelFromRepo);
            _employeeRepository.UpdateEmployee(employeeModelFromRepo);
            _employeeRepository.SaveChanges();

            return NoContent();
        }
        
        //[Authorize(Policy = "Employee")]
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            // check if the deletable assignment exists
            var employeeModelFromRepo = _employeeRepository.GetEmployeeById(id);
            if (employeeModelFromRepo == null)
            {
                return NotFound();
            }
            
            _employeeRepository.DeleteEmployee(employeeModelFromRepo);
            _employeeRepository.SaveChanges();

            return NoContent();
        }
        
        // find all assignments of an employee
        [HttpGet("{id}/assignments", Name = "FindAssignmentsOfEmployee")]
        public ActionResult<IEnumerable<AssignmentDto>> FindAssignmentsOfEmployee(int id)
        {
            // var employee = _employeeRepository.GetEmployeeById(id);

            var employee = _employeeRepository.GetAssignmentsOfEmployee(id);
            return Ok(_mapper.Map<IEnumerable<AssignmentDto>>(employee));
        }
    }
}