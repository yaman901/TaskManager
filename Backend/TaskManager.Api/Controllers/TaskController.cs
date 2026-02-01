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
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDTO>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.AssignedUser)
                .ToListAsync();

            var tasksDTO = _mapper.Map<List<TaskItemDTO>>(tasks);
            return Ok(tasksDTO);
        }

        // GET: api/task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDTO>> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.AssignedUser)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return NotFound();

            var taskDTO = _mapper.Map<TaskItemDTO>(task);
            return Ok(taskDTO);
        }

        // POST: api/task
        [HttpPost]
        public async Task<ActionResult<TaskItemDTO>> CreateTask(CreateTaskItemDTO createTaskDTO)
        {
            var task = _mapper.Map<TaskItem>(createTaskDTO);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var taskDTO = _mapper.Map<TaskItemDTO>(task);

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, taskDTO);
        }

        // PUT: api/task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItemDTO taskDTO)
        {
            if (id != taskDTO.Id)
                return BadRequest();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _mapper.Map(taskDTO, task);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
