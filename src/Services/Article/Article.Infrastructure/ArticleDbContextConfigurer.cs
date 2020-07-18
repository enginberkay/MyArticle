using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Infrastructure
{
    public static class ArticleDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ArticleDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }
    }
}
