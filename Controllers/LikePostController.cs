using BlogApi.Dtos;
using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.IServices;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikePostController : ControllerBase
    {
        private readonly ILikePostService _likePostService;

        // Construtor que injeta o serviço de likes em posts
        public LikePostController(ILikePostService likePostService)
        {
            _likePostService = likePostService;
        }

        // GET: api/LikePost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikePostDTO>>> Index()
        {
            var likePosts = await _likePostService.GetAllLikePostsAsync();
            var likePostDtos = likePosts.Select(lp => new LikePostDTO
            {
                UserId = lp.UserId,
                PostId = lp.PostId
            });

            return Ok(likePostDtos);
        }

        // GET: api/LikePost/{userId}/{postId}
        [HttpGet("{userId}/{postId}")]
        public async Task<ActionResult<LikePostDTO>> Details(string userId, string postId)
        {
            var likePost = await _likePostService.GetLikePostByIdAsync(userId, postId);
            if (likePost == null)
            {
                return NotFound();
            }

            var likePostDto = new LikePostDTO
            {
                UserId = likePost.UserId,
                PostId = likePost.PostId
            };

            return Ok(likePostDto);
        }

        // POST: api/LikePost
        [HttpPost]
        public async Task<ActionResult<LikePostDTO>> Create([FromBody] LikePostDTO likePostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var likePost = new LikePost
            {
                UserId = likePostDto.UserId,
                PostId = likePostDto.PostId,
                // Assuming you need to add more properties to LikePost and handle Post creation/fetching separately
            };

            await _likePostService.AddLikePostAsync(likePost);
            return CreatedAtAction(nameof(Details), new { userId = likePost.UserId, postId = likePost.PostId }, likePostDto);
        }

        // PUT: api/LikePost/{userId}/{postId}
        [HttpPut("{userId}/{postId}")]
        public async Task<IActionResult> Edit(string userId, string postId, [FromBody] LikePostDTO likePostDto)
        {
            if (userId != likePostDto.UserId || postId != likePostDto.PostId)
            {
                return BadRequest("The provided UserId and PostId do not match the URL parameters.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var likePost = new LikePost
            {
                UserId = likePostDto.UserId,
                PostId = likePostDto.PostId,
                // Assuming you need to add more properties to LikePost and handle Post creation/fetching separately
            };

            await _likePostService.UpdateLikePostAsync(likePost);
            return NoContent();
        }

        // DELETE: api/LikePost/{userId}/{postId}
        [HttpDelete("{userId}/{postId}")]
        public async Task<IActionResult> Delete(string userId, string postId)
        {
            try
            {
                await _likePostService.DeleteLikePostAsync(userId, postId);
                return NoContent();
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete like post. Try again, and if the problem persists, see your system administrator.");
                return BadRequest(ModelState);
            }
        }
    }
}