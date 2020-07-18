using Content.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Infrastructure.Configurations
{
    public class ArticleKeywordConfiguration: IEntityTypeConfiguration<ArticleKeyword>
    {
        public void Configure(EntityTypeBuilder<ArticleKeyword> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .HasIndex(m => m.Article)
                .HasName("IDX_ARTICLE");
        }
    }
}
