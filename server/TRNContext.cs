using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TRNServer
{
    public class TRNContext : DbContext
    {
        public TRNContext(DbContextOptions<TRNContext> options) : base(options) { }

        
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }

    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}