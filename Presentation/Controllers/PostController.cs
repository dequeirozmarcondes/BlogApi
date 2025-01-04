using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Application.IServices;
using BlogApi.Application.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostListDto>>> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            var postDtos = posts.Select(post => new PostListDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId // Incluindo UserId
            }).ToList();

            return Ok(postDtos);
        }

        // GET: api/Post/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDetailsDto>> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty.");
            }

            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var postDto = new PostDetailsDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId // Incluindo UserId
            };

            return Ok(postDto);
        }

        // POST: api/Post
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] PostCreateDto postCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post
            {
                Title = postCreateDto.Title,
                Content = postCreateDto.Content,
                UserId = postCreateDto.UserId
            };

            await _postService.AddPostAsync(post);

            return CreatedAtAction(nameof(Details), new { id = post.Id }, post);
        }

        // PUT: api/Post/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] PostEditDto postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest("The provided ID does not match the ID in the body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            existingPost.Title = postDto.Title;
            existingPost.Content = postDto.Content;

            await _postService.UpdatePostAsync(existingPost);
            return NoContent();
        }

        // DELETE: api/Post/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var existingPost = await _postService.GetPostByIdAsync(id);
                if (existingPost == null)
                {
                    return NotFound();
                }

                await _postService.DeletePostAsync(id);
                return NoContent();
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete post. Try again, and if the problem persists, see your system administrator.");
                return BadRequest(ModelState);
            }
        }
    }
}