using System;
using System.Collections.Generic;

namespace BlogApi.Dtos
{
    // DTOs relacionados a Posts
    public class PostDTO
    {
        // DTO para listar posts
        public class PostListDto
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        // DTO para detalhes de um post
        public class PostDetailsDto
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        // DTO para criação de um post
        public class PostCreateDto
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }

        // DTO para edição de um post
        public class PostEditDto
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        // DTO para exclusão de um post
        public class PostDeleteDto
        {
            public string Id { get; set; }
        }
    }

    // DTO relacionado a Likes em posts
    public class LikePostDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
    }

    // DTO relacionado a Comentários em posts
    public class CommentsPostDTO
    {
        public string Id { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // DTO relacionado a Posts de usuários
    public class UserPostDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
    }

    // DTOs relacionados a ApplicationUser
    public class ApplicationUserDTO
    {
        // DTO para listar usuários
        public class UserListDto
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }

        // DTO para detalhes de um usuário
        public class UserDetailsDto
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public IEnumerable<PostDTO.PostListDto> Posts { get; set; }
        }

        // DTO para criação de um usuário
        public class UserCreateDto
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        // DTO para edição de um usuário
        public class UserEditDto
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }

        // DTO para exclusão de um usuário
        public class UserDeleteDto
        {
            public string Id { get; set; }
        }
    }
}