using BlogApi.Application.IServices;
using BlogApi.Core.Entities;
using BlogApi.Core.IRepository;
using System.Web;

namespace BlogApi.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILikePostRepository _likePostRepository;
        private readonly ICommentsPostRepository _commentsPostRepository;

        public PostService(
            IPostRepository postRepository,
            ILikePostRepository likePostRepository,
            ICommentsPostRepository commentsPostRepository)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _likePostRepository = likePostRepository ?? throw new ArgumentNullException(nameof(likePostRepository));
            _commentsPostRepository = commentsPostRepository ?? throw new ArgumentNullException(nameof(commentsPostRepository));
        }

        // Retorna todos os posts
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            foreach (var post in posts)
            {
                post.LikePosts = (await _likePostRepository.GetLikePostsByPostIdAsync(post.Id))?.ToList() ?? [];
                post.CommentsPosts = (await _commentsPostRepository.GetCommentsPostsByPostIdAsync(post.Id))?.ToList() ?? [];
            }
            return posts;
        }

        // Retorna um post específico pelo ID
        public async Task<Post> GetPostByIdAsync(string id)
        {
            // Converter o ID recebido para o formato esperado pelo RavenDB
            string decodedId = HttpUtility.UrlDecode(id);
            var post = await _postRepository.GetPostByIdAsync(decodedId);

            if (post != null)
            {
                post.LikePosts = (await _likePostRepository.GetLikePostsByPostIdAsync(post.Id))?.ToList() ?? [];
                post.CommentsPosts = (await _commentsPostRepository.GetCommentsPostsByPostIdAsync(post.Id))?.ToList() ?? [];
            }
            return post;
        }

        // Adiciona um novo post
        public async Task AddPostAsync(Post post)
        {
            ArgumentNullException.ThrowIfNull(post);

            await _postRepository.AddPostAsync(post);
        }

        // Atualiza um post existente
        public async Task UpdatePostAsync(Post post)
        {
            ArgumentNullException.ThrowIfNull(post);

            await _postRepository.UpdatePostAsync(post);
        }

        // Deleta um post pelo ID
        public async Task DeletePostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            string decodedId = HttpUtility.UrlDecode(id);
            await _postRepository.DeletePostAsync(decodedId);
        }
    }
}