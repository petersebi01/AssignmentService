using System.Collections.Generic;
using AssignmentService.Data;
using AssignmentService.Dto;
using AssignmentService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentService.Controllers
{
    [Route("api/works")]
    [ApiController]
    public class WorkResource : ControllerBase
    {
        private readonly IWorkRepository _workRepository;
        private readonly IMapper _mapper;

        public WorkResource(IWorkRepository workRepository, IMapper mapper)
        {
            _workRepository = workRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkDto>> FindAllWorks()
        {
            var work = _workRepository.GetAllWorks();
            return Ok(_mapper.Map<IEnumerable<WorkDto>>(work));
        }

        [HttpGet("{id}", Name = "FindWorkById")]
        public ActionResult<WorkDto> FindWorkById(int id)
        {
            var work = _workRepository.GetWorkById(id);
            if (work != null)
            {
                return Ok(_mapper.Map<WorkDto>(work)); 
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<WorkDto> CreateWork(WorkDto workDto)
        {
            var workToCreate = _mapper.Map<Work>(workDto);
            _workRepository.CreateWork(workToCreate);
            _workRepository.SaveChanges();
            
            var createdWork = _mapper.Map<WorkDto>(workToCreate);
            return CreatedAtRoute(nameof(FindWorkById), new {Id = createdWork.WorkId}, workDto);
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateWork(int id, WorkDto workDto)
        {
            var workModelFromRepo = _workRepository.GetWorkById(id);
            if (workModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(workDto, workModelFromRepo);
            _workRepository.UpdateWork(workModelFromRepo);
            _workRepository.SaveChanges();

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteWork(int id)
        {
            var workModelFromRepo = _workRepository.GetWorkById(id);
            if (workModelFromRepo == null)
            {
                return NotFound();
            }
            
            _workRepository.DeleteWork(workModelFromRepo);
            _workRepository.SaveChanges();

            return NoContent();
        }
    }
}