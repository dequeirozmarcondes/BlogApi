namespace BlogApi.Dtos
{
    public class PostDTO
    {
        public class PostListDto
        {
            public string Id { get; set; }
            public string Title { get; set; }
        }

        public class PostDetailsDto
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class PostCreateDto
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class PostEditDto
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class PostDeleteDto
        {
            public string Id { get; set; }
        }
    }
}
