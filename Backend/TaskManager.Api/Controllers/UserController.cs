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
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            return Ok(usersDTO);
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO createUserDTO)
        {
            // NOTE: Hash password here in a real app
            var user = _mapper.Map<User>(createUserDTO);
            user.PasswordHash = createUserDTO.Password; // Replace with actual hash logic!

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDTO = _mapper.Map<UserDTO>(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDTO);
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
                return BadRequest();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            // Map updated fields from DTO to entity
            _mapper.Map(userDTO, user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
