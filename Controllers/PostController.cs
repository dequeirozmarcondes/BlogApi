using BlogApi.Dtos;
using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlogApi.Dtos.PostDTO;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

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
                Title = post.Title
            });

            return Ok(postDtos);
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDetailsDto>> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
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
        public async Task<ActionResult<PostCreateDto>> Create([Bind("Title,Content")] PostCreateDto postDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var post = new Post
                    {
                        Title = postDto.Title,
                        Content = postDto.Content
                    };

                    await _postService.AddPostAsync(post);
                    return CreatedAtAction(nameof(Details), new { id = post.Id }, postDto);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Content")] PostEditDto postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var post = new Post
                    {
                        Id = postDto.Id,
                        Title = postDto.Title,
                        Content = postDto.Content
                    };

                    await _postService.UpdatePostAsync(post);
                    return NoContent();
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Post/5
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