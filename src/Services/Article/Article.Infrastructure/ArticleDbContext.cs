using Content.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Content.Infrastructure
{
    public class ArticleDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleKeyword> ArticleKeywords { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ArticleDbContext(DbContextOptions<ArticleDbContext> options)
            : base(options)
        {
        }
    }
}
