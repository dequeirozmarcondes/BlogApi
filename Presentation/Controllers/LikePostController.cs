using BlogApi.Application.Dtos;
using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlogApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikePostController(ILikePostService likePostService) : ControllerBase
    {
        private readonly ILikePostService _likePostService = likePostService ?? throw new ArgumentNullException(nameof(likePostService));

        // GET: api/LikePost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikePostDto>>> Index()
        {
            var likePosts = await _likePostService.GetAllLikePostsAsync();
            var likePostDtos = likePosts.Select(lp => new LikePostDto
            {
                UserId = lp.UserId,
                PostId = lp.PostId
            }).ToList();

            return Ok(likePostDtos);
        }

        // GET: api/LikePost/{userId}/{postId}
        [HttpGet("{userId}/{postId}")]
        public async Task<ActionResult<LikePostDto>> Details(string userId, string postId)
        {
            string decodedUserId = HttpUtility.UrlDecode(postId);

            var likePost = await _likePostService.GetLikePostByIdAsync(userId, decodedUserId);
            if (likePost == null)
            {
                return NotFound();
            }

            var likePostDto = new LikePostDto
            {
                UserId = likePost.UserId,
                PostId = likePost.PostId
            };

            return Ok(likePostDto);
        }

        // POST: api/LikePost
        [HttpPost]
        public async Task<ActionResult<LikePostDto>> Create([FromBody] LikePostDto likePostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var likePost = new LikePost
            {
                UserId = likePostDto.UserId,
                PostId = likePostDto.PostId
            };

            await _likePostService.AddLikePostAsync(likePost);
            return CreatedAtAction(nameof(Details), new { userId = likePost.UserId, postId = likePost.PostId }, likePostDto);
        }

        // PUT: api/LikePost/{userId}/{postId}
        [HttpPut("{userId}/{postId}")]
        public async Task<IActionResult> Edit(string userId, string postId, [FromBody] LikePostDto likePostDto)
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
                PostId = likePostDto.PostId
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