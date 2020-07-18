using Content.Domain;
using Content.Domain.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Content.Infrastructure
{
    class ArticleDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ArticleDbContext>
    {
        public ArticleDbContext CreateDbContext(string[] args)
        {
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            var builder = new DbContextOptionsBuilder<ArticleDbContext>();

            ArticleDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AppConst.ConnectionStringName));

            return new ArticleDbContext(builder.Options);
        }
    }
}
