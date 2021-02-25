using System.Collections.Generic;
using AssignmentService.Data;
using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentService.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksResource : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TasksResource(ITaskRepository taskTaskRepository, IMapper mapper)
        {
            _taskRepository = taskTaskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskDto>> FindAllTasks()
        {
            var tasks = _taskRepository.GetAllTasks();
            return Ok(_mapper.Map<IEnumerable<TaskDto>>(tasks));
        }

        [HttpGet("{id}", Name = "FindTaskById")]
        public ActionResult<TaskDto> FindTaskById(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            if (task != null)
            {
                return Ok(_mapper.Map<TaskDto>(task)); 
            }

            return NotFound();
        }
        
        [HttpPost]
        public ActionResult<TaskDto> CreateTask(TaskDto taskDto)
        {
            var taskToCreate = _mapper.Map<Task>(taskDto);
            _taskRepository.CreateTask(taskToCreate);
            _taskRepository.SaveChanges();

            // sync communication here with user service
            var createdTask = _mapper.Map<TaskDto>(taskToCreate);
            return CreatedAtRoute(nameof(FindTaskById), new {Id = createdTask.TaskId}, taskDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, TaskDto taskDto)
        {
            var taskModelFromRepo = _taskRepository.GetTaskById(id);
            if (taskModelFromRepo == null)
            {
                return NotFound();
            }
            
            _mapper.Map(taskDto, taskModelFromRepo);
            _taskRepository.UpdateTask(taskModelFromRepo);
            _taskRepository.SaveChanges();

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var taskModelFromRepo = _taskRepository.GetTaskById(id);
            if (taskModelFromRepo == null)
            {
                return NotFound();
            }
            
            _taskRepository.DeleteTask(taskModelFromRepo);
            _taskRepository.SaveChanges();

            return NoContent();
        }
        
        [HttpGet("{id}/employees", Name = "FindEmployeesOnTask")]
        public ActionResult<IEnumerable<EmployeeDto>> FindEmployeesOnTask(int id)
        {
            var employeesOnTask = _taskRepository.GetEmployeesOnTask(id);
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employeesOnTask));
        }
        
        [HttpGet("{id}/assignments", Name = "FindAssignmentsOnTask")]
        public ActionResult<IEnumerable<AssignmentDto>> FindAssignmentsOnTask(int id)
        {
            var assignmentsOnTask = _taskRepository.GetAssignmentsOnTask(id);
            return Ok(_mapper.Map<IEnumerable<AssignmentDto>>(assignmentsOnTask));
        }
    }
}