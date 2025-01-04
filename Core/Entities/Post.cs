using System;
using System.Collections.Generic;

namespace BlogApi.Core.Entities
{
    public class Post
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required bool Published { get; set; }
        public required DateTime PublishedAt { get; set; }

        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }

        public ICollection<CommentsPost> CommentsPosts { get; set; } = new List<CommentsPost>();
        public ICollection<LikePost> LikePosts { get; set; } = new List<LikePost>();

        // Construtor padrão
        public Post()
        {
        }

        // Construtor completo
        public Post(string id, string title, string content, bool published, DateTime publishedAt, string userId, ApplicationUser user)
        {
            Id = id;
            Title = title;
            Content = content;
            Published = published;
            PublishedAt = publishedAt;
            UserId = userId;
            User = user;
            CommentsPosts = new List<CommentsPost>();
            LikePosts = new List<LikePost>();
        }

        // Construtor sem atributos Published e PublishedAt
        public Post(string id, string title, string content, string userId, ApplicationUser user)
        {
            Id = id;
            Title = title;
            Content = content;
            Published = false; // Valor padrão para Published
            PublishedAt = DateTime.MinValue; // Valor padrão para PublishedAt
            UserId = userId;
            User = user;
            CommentsPosts = new List<CommentsPost>();
            LikePosts = new List<LikePost>();
        }

        // Construtor mínimo para inicialização básica
        public Post(string id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
            Published = false; // Valor padrão para Published
            PublishedAt = DateTime.MinValue; // Valor padrão para PublishedAt
            CommentsPosts = new List<CommentsPost>();
            LikePosts = new List<LikePost>();
        }
    }
}