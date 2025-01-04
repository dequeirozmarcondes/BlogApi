using BlogApi.Application.Dtos;
using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController(IApplicationUserService userService) : ControllerBase
    {
        private readonly IApplicationUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        // GET: api/ApplicationUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var userDtos = users.Select(user => new UserListDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty, // Fix for possible null reference
                Email = user.Email ?? string.Empty // Fix for possible null reference
            }).ToList();

            return Ok(userDtos);
        }

        // GET: api/ApplicationUser/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDetailsDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty, // Fix for possible null reference
                Email = user.Email ?? string.Empty, // Fix for possible null reference
                Posts = user.Posts.Select(post => new PostListDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content
                }).ToList()
            };

            return Ok(userDto);
        }

        // POST: api/ApplicationUser
        [HttpPost]
        public async Task<ActionResult<UserListDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = userCreateDto.UserName ?? string.Empty, // Fix for possible null reference
                Email = userCreateDto.Email ?? string.Empty // Fix for possible null reference
            };

            // Note: Password should be hashed and managed via UserManager for security
            await _userService.AddUserAsync(user);

            var userDto = new UserListDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty, // Fix for possible null reference
                Email = user.Email ?? string.Empty // Fix for possible null reference
            };

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
        }

        // PUT: api/ApplicationUser/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserEditDto userEditDto)
        {
            if (id != userEditDto.Id)
            {
                return BadRequest("User ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Id = userEditDto.Id,
                UserName = userEditDto.UserName ?? string.Empty, // Fix for possible null reference
                Email = userEditDto.Email ?? string.Empty // Fix for possible null reference
            };

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        // DELETE: api/ApplicationUser/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete user. Try again, and if the problem persists, see your system administrator.");
                return BadRequest(ModelState);
            }
        }
    }
}