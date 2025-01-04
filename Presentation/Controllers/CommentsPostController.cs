using BlogApi.Application.Dtos;
using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsPostController(ICommentsPostService commentsPostService) : ControllerBase
    {
        private readonly ICommentsPostService _commentsPostService = commentsPostService ?? throw new ArgumentNullException(nameof(commentsPostService));

        // GET: api/CommentsPost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentsPostDto>>> Index()
        {
            var commentsPosts = await _commentsPostService.GetAllCommentsPostsAsync();
            var commentsPostDtos = commentsPosts.Select(cp => new CommentsPostDto
            {
                Id = cp.Id,
                PostId = cp.PostId,
                UserId = cp.UserId,
                Content = cp.Content,
                CreatedAt = cp.CreatedAt
            }).ToList();

            return Ok(commentsPostDtos);
        }

        // GET: api/CommentsPost/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentsPostDto>> Details(string id)
        {
            var commentsPost = await _commentsPostService.GetCommentsPostByIdAsync(id);
            if (commentsPost == null)
            {
                return NotFound();
            }

            var commentsPostDto = new CommentsPostDto
            {
                Id = commentsPost.Id,
                PostId = commentsPost.PostId,
                UserId = commentsPost.UserId,
                Content = commentsPost.Content,
                CreatedAt = commentsPost.CreatedAt
            };

            return Ok(commentsPostDto);
        }

        // POST: api/CommentsPost
        [HttpPost]
        public async Task<ActionResult<CommentsPostDto>> Create([FromBody] CommentsPostDto commentsPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentsPost = new CommentsPost
            {
                PostId = commentsPostDto.PostId,
                UserId = commentsPostDto.UserId,
                Content = commentsPostDto.Content,
                CreatedAt = commentsPostDto.CreatedAt
            };

            await _commentsPostService.AddCommentsPostAsync(commentsPost);
            return CreatedAtAction(nameof(Details), new { id = commentsPost.Id }, commentsPostDto);
        }

        // PUT: api/CommentsPost/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] CommentsPostDto commentsPostDto)
        {
            if (id != commentsPostDto.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentsPost = new CommentsPost
            {
                Id = commentsPostDto.Id,
                PostId = commentsPostDto.PostId,
                UserId = commentsPostDto.UserId,
                Content = commentsPostDto.Content,
                CreatedAt = commentsPostDto.CreatedAt
            };

            await _commentsPostService.UpdateCommentsPostAsync(commentsPost);
            return NoContent();
        }

        // DELETE: api/CommentsPost/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _commentsPostService.DeleteCommentsPostAsync(id);
                return NoContent();
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete comments post. Try again, and if the problem persists, see your system administrator.");
                return BadRequest(ModelState);
            }
        }
    }
}