using AutoMapper;
using EfCoreSample.Doman;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman.Entities;
using EfCoreSample.Infrastructure.Abstraction;
using EfCoreSample.Infrastructure.Extensions;
using EfCoreSample.Infrastructure.SortingPaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
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
      
        [HttpGet]
        public ActionResult<List<ProjectGetDto>> Get(string sort,  int? pageSize, int? pageNumber,
                    string status, string title, string startTime, string endTime)
        {
            IEnumerable<Project> projects = _dbService.Get(status, title, startTime, endTime);
            projects.GetSorted(sort);
            projects = projects.GetPaginated(pageSize, pageNumber);
            return _mapper.Map<List<ProjectGetDto>>(projects);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectGetDto>> Get(long id)
        {
            var entity = await _dbService.FindAsync(id);
            if (entity == null) return NotFound();
            return _mapper.Map<ProjectGetDto>(entity);
        }


        [HttpGet("{id}/members")]
        public ActionResult<List<EmployeeDTO>> Get(long id, bool members = true)
        {    
            var entity = _dbService.GetRelated<Employee>(id);
            if (entity == null) return NotFound();
            return _mapper.Map<List<EmployeeDTO>>(entity);
        }


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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {  
            var exists = await _dbService.AnyAsync(id);
            if (!exists) return NotFound();
            var result = await _dbService.DeleteAsync(id);
            if (!result.Success) return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            return NoContent();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(ProjectPutDto projectDTO) 
        {
            
            if (projectDTO == null) return BadRequest("No entity provided");
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
