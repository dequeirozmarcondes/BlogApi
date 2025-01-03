using BlogApi.Core.Entities;
using BlogApi.Dtos;
using BlogApi.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        public ApplicationUserController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        // GET: api/ApplicationUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUserDTO.UserListDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var userDtos = users.Select(user => new ApplicationUserDTO.UserListDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            });

            return Ok(userDtos);
        }

        // GET: api/ApplicationUser/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDTO.UserDetailsDto>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new ApplicationUserDTO.UserDetailsDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Posts = user.Posts.Select(post => new PostDTO.PostListDto
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
        public async Task<ActionResult<ApplicationUserDTO.UserListDto>> CreateUser([FromBody] ApplicationUserDTO.UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = userCreateDto.UserName,
                Email = userCreateDto.Email
            };

            // Note: Password should be hashed and managed via UserManager for security
            await _userService.AddUserAsync(user);

            var userDto = new ApplicationUserDTO.UserListDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userDto);
        }

        // PUT: api/ApplicationUser/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] ApplicationUserDTO.UserEditDto userEditDto)
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
                UserName = userEditDto.UserName,
                Email = userEditDto.Email
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