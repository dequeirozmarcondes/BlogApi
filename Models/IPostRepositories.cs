namespace BlogApi.Models
{
    public interface IPostRepositories
    {
        public interface IPostRepository
        {
            Task<IEnumerable<Post>> GetAllPostsAsync();
            Task<Post> GetPostByIdAsync(string id);
            Task AddPostAsync(Post post);
            Task UpdatePostAsync(Post post);
            Task DeletePostAsync(string id);
        }
    }
}
