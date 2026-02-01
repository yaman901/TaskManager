using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.CreatedByUser)
                .ToListAsync();

            var projectsDTO = _mapper.Map<List<ProjectDTO>>(projects);
            return Ok(projectsDTO);
        }

        // GET: api/project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.CreatedByUser)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            var projectDTO = _mapper.Map<ProjectDTO>(project);
            return Ok(projectDTO);
        }

        // POST: api/project
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject(CreateProjectDTO createProjectDTO)
        {
            var project = _mapper.Map<Project>(createProjectDTO);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var projectDTO = _mapper.Map<ProjectDTO>(project);

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, projectDTO);
        }

        // PUT: api/project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDTO projectDTO)
        {
            if (id != projectDTO.Id)
                return BadRequest();

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            _mapper.Map(projectDTO, project);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
