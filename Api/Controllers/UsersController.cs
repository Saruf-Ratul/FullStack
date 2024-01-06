using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // api/users
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet ("GetAll")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }       

        [HttpPost ("Create")]
        public async Task<IActionResult> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new ObjectResult(user);
        }

        [HttpGet("Fetch{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpDelete("Delete{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return new ObjectResult(user);
        }
        [HttpPut("Update{id:int}")]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ObjectResult(user);
        }

        [HttpGet("FetchByEmail{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }   

        [HttpGet("FetchByName{name}")]
        public async Task<ActionResult<User>> GetUserByName(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }   

        [HttpGet("FetchByPhoneNumber{phoneNumber}")]
        public async Task<ActionResult<User>> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("FetchByAddress{address}")]
        public async Task<ActionResult<User>> GetUserByAddress(string address)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Address == address);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }



        

        
        
    }
}