using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using BlogApi.Dtos; // Adicione esta diretiva
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlogApi.Dtos.PostDTO;
using BlogApi.IServices;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        // Construtor que injeta o serviço de posts
        public PostController(IPostService postService)
        {
            _postService = postService;
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
                Content = post.Content
            });

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
                Content = post.Content
            };

            return Ok(postDto);
        }

        // POST: api/Post
        [HttpPost]
        public async Task<ActionResult<PostCreateDto>> Create([FromBody] PostCreateDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post(
                Guid.NewGuid().ToString(), // Generate a new ID
                postDto.Title,
                postDto.Content
            );

            await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(Details), new { id = post.Id }, postDto);
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

            var post = new Post(
                postDto.Id,
                postDto.Title,
                postDto.Content
            );

            await _postService.UpdatePostAsync(post);
            return NoContent();
        }

        // DELETE: api/Post/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
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