using crud.Model;
using crud.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepo.GetUserAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id) 
        {
            var users = await _userRepo.GetByIdAsync(id);
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (user == null) return BadRequest("Enter the User Details..!");

            var createdUser = await _userRepo.CreatedAsync(user);
            return Ok(createdUser);

            //return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null) return BadRequest();

            var UpdatedUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
            var Success =await _userRepo.UpdateAsync(id, user);

            if(!Success) return NotFound();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        { 
            var Success= await _userRepo.DeleteAsync(id);
            if(!Success) return NotFound();
            return NoContent();

        }
    }
}
