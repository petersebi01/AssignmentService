using System.Collections.Generic;
using AssignmentService.Data;
using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentService.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentsResource : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public AssignmentsResource(IAssignmentRepository assignmentAssignmentRepository, IEmployeeRepository employeeRepository, ITaskRepository taskRepository, IMapper mapper)
        {
            _assignmentRepository = assignmentAssignmentRepository;
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AssignmentDto>> FindAllAssignment()
        {
            var assignments = _assignmentRepository.GetAllAssignments();
            return Ok(_mapper.Map<IEnumerable<AssignmentDto>>(assignments));
        }

        [HttpGet("{id}", Name = "FindAssignmentById")]
        public ActionResult<AssignmentDto> FindAssignmentById(int id)
        {
            var assignment = _assignmentRepository.GetAssignmentById(id);
            if (assignment != null)
            {
                return Ok(_mapper.Map<AssignmentDto>(assignment)); 
            }

            return NotFound();
        }

        // find the task on the assignment
        [HttpGet("{id}/task", Name = "FindTaskOnAssignment")]
        public ActionResult<TaskDto> FindTaskOnAssignment(int id)
        {
            var task = _assignmentRepository.GetTaskOnAssignment(id);
            return Ok(_mapper.Map<TaskDto>(task));
        }

        // find all employees on an assignment
        [HttpGet("{id}/employees", Name = "FindEmployeesOnAssignments")]
        public ActionResult<IEnumerable<EmployeeDto>> FindEmployeesOnAssignment(int id)
        {
        var employees = _assignmentRepository.GetEmployeesOnAssignment(id);
        return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpPost]
        public ActionResult<AssignmentDto> CreateAssignment(AssignmentDto assignmentDto)
        {
            var assignmentToCreate = _mapper.Map<Assignment>(assignmentDto);
         
            // check if the referenced task exists
            // var task = _taskRepository.GetTaskById(assignmentToCreate.Task.TaskId);
            // if (task == null) return NotFound();
            
            _assignmentRepository.CreateAssignment(assignmentToCreate);
            _assignmentRepository.SaveChanges();
                
            var assignmentCreated = _mapper.Map<AssignmentDto>(assignmentToCreate);
            return CreatedAtRoute(nameof(FindAssignmentById), new {Id = assignmentCreated.AssignmentId},
                assignmentDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAssignment(int id, AssignmentDto assignmentDto)
        {
            // check if the updatable assignment exists
            var assignmentModelFromRepo = _assignmentRepository.GetAssignmentById(id);
            if (assignmentModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(assignmentDto, assignmentModelFromRepo);
            _assignmentRepository.UpdateAssignment(assignmentModelFromRepo);
            _assignmentRepository.SaveChanges();

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteAssignment(int id)
        {
            // check if the deletable assignment exists
            var assignmentModelFromRepo = _assignmentRepository.GetAssignmentById(id);
            if (assignmentModelFromRepo == null)
            {
                return NotFound();
            }
            
            _assignmentRepository.DeleteAssignment(assignmentModelFromRepo);
            _assignmentRepository.SaveChanges();

            return NoContent();
        }
    }
}