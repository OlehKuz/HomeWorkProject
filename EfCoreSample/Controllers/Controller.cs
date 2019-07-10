using AutoMapper;
using EfCoreSample.Doman;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman.Entities;
using EfCoreSample.Infrastructure.Abstraction;
using EfCoreSample.Infrastructure.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EfCoreSample.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IService<Project, long> _dbService;
        private readonly LinkGenerator _link;
        private readonly IMapper _mapper;


        public ProjectController(IService<Project, long> dbService, IMapper mapper, LinkGenerator link)
        {
            _dbService = dbService;
            _link = link;
            _mapper = mapper;
        }

        // GET api/Project
        [HttpGet]
        public ActionResult<List<ProjectGetDto>> Get(string sort, 
            int? pageNumber,  int? pageSize, string status, string title, string startTime, string endTime)
        {
            
            var projects = _dbService.Get(sort, pageNumber, pageSize, 
                status, title, startTime, endTime);
            return _mapper.Map<List<ProjectGetDto>>(projects);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectGetDto>> Get(long id)
        {
            var entity = await _dbService.FindAsync(id);
            if (entity == null) return NotFound();
            return _mapper.Map<ProjectGetDto>(entity);
        }

        /// <summary>
        /// Retrieves Employees that are Members of selected project
        /// </summary>
        [HttpGet("{id}/members")]
        public ActionResult<List<EmployeeDTO>> Get(long id, bool members = true)
        {    
            var entity = _dbService.GetRelated<Employee>(id);
            if (entity == null) return NotFound();
            return _mapper.Map<List<EmployeeDTO>>(entity);

        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Project
        ///     "title": "Title post project",
        ///     "status": "Pending",
        ///     "lastUpdated": "2019-07-09",
        ///     "startTime": "2019-07-06",
        ///     "endTime": "2019-08-25"
        ///
        /// </remarks>
        /// 
        /// <returns>A newly created Project</returns>
        /// <response code="201">Returns the newly created project</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        public async Task<ActionResult<ProjectGetDto>> Post(ProjectPostDto saveDto)
        {
            if (saveDto == null) return BadRequest("No entity provided");
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var entity = _mapper.Map<Project>(saveDto);
            var result = await _dbService.InsertAsync(entity);
            if(!result.Success)return StatusCode(StatusCodes.Status500InternalServerError,result.Message);
            var posted = result.Entity;
            return Created(_link.GetPathByAction("Get", "Project", new { posted.Id }), saveDto);
        }


        // PUT api/Project/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, ProjectPutDto projectDTO)
        {
            if (projectDTO == null) return BadRequest("No entity provided");
            if (!id.Equals(projectDTO.Id)) return BadRequest("Differing ids");
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var exists = await _dbService.AnyAsync(id);
            if (!exists) return NotFound();
            var entity = _mapper.Map<Project>(projectDTO);
            var result = await _dbService.Update(entity);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }
        //TODO no validation at all
        // PUT api/Project/5
        [HttpPut]
        public async Task<ActionResult> Put(List<ProjectPutDto> projectDTO)
        {
            //TODO Validation
            if (projectDTO == null) return BadRequest("No entity provided");
            
            var entity = _mapper.Map<List<Project>>(projectDTO);
            var result = await _dbService.UpdateRange(entity);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Project.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {  
            var exists = await _dbService.AnyAsync(id);
            if (!exists) return NotFound();
            var result = await _dbService.DeleteAsync(id);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }

        /// <summary>
        /// Deletes a supplied Project.
        /// </summary>
        [HttpDelete("{id}/project")]
        public async Task<ActionResult> Delete(long id, ProjectPutDto projectDTO) 
        {
            //TODO it doesnt check if passed item content correstponds to item we deleted, checks only id
            if (projectDTO == null) return BadRequest("No entity provided");
            if (!id.Equals(projectDTO.Id)) return BadRequest("Differing ids");
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var entity = _mapper.Map<Project>(projectDTO);
            var exists = await _dbService.AnyAsync(entity.Id);
            if (!exists) return NotFound();           
            var result = await _dbService.DeleteAsync(entity);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }
    }
}
