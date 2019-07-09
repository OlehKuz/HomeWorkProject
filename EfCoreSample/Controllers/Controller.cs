using AutoMapper;
using EfCoreSample.Doman;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman.Entities;
using EfCoreSample.Infrastructure.Abstraction;
using EfCoreSample.Infrastructure.Extensions;
using EfCoreSample.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreSample.Controllers
{
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

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> Get()
        {
            try
            {
                var projects= await _dbService.GetAsync(p=>p.Id==2);
                return _mapper.Map<List<ProjectDTO>>(projects);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Database Failure");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> Get(long id)
        {

            var entity = await _dbService.FindAsync(id);
            if (entity == null) return NotFound();
            return _mapper.Map<ProjectDTO>(entity);

        }

        // GET api/values/5
        // GET api/values/5
        [HttpGet("{id}/members")]
        public async Task<ActionResult<List<EmployeeDTO>>> Get(long id, bool members = true)
        {
            
                var entity = await _dbService.GetRelated<Employee>(id);
                if (entity == null) return NotFound();
                return _mapper.Map<List<EmployeeDTO>>(entity);
           
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> Post(SaveProjectDTO saveDto)
        {
            if (saveDto == null) return BadRequest("No entity provided");
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var entity = _mapper.Map<Project>(saveDto);
            var result = await _dbService.InsertAsync(entity);
            if(!result.Success)return StatusCode(StatusCodes.Status500InternalServerError,result.Message);
            var posted = result.Entity;
            return Created(_link.GetPathByAction("Get", "Project", new { posted.Id }), posted);
        }
    

        // PUT api/values/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, ProjectDTO projectDTO)
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

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(List<ProjectDTO> projectDTO)
        {
            //TODO Validation
            if (projectDTO == null) return BadRequest("No entity provided");
            
            var entity = _mapper.Map<List<Project>>(projectDTO);
            var result = await _dbService.UpdateRange(entity);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }
        // DELETE api/values/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(long id)
        {  
            var exists = await _dbService.AnyAsync(id);
            if (!exists) return NotFound();
            var result = await _dbService.DeleteAsync(id);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, ProjectDTO projectDTO)
        {
            //TODO it doesnt check if passed item content correstponds to item we deleted, checks only id
            if (projectDTO == null) return BadRequest("No entity provided");
            if (!id.Equals(projectDTO.Id)) return BadRequest("Differing ids");
            var entity = _mapper.Map<Project>(projectDTO);
            var exists = await _dbService.AnyAsync(entity.Id);
            if (!exists) return NotFound();           
            var result = await _dbService.DeleteAsync(entity);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }
    }
}
