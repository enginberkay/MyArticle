using Content.Domain.Models;
using Content.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Content.Infrastructure.Repositories
{
    public class ArticleKeyWordRepository : Repository<ArticleKeyword>, IArticleKeyWordRepository
    {
        public ArticleKeyWordRepository(ArticleDbContext context)
            : base(context)
        { }

        public IEnumerable<Article> GetArticlesByKeyWord(Expression<Func<ArticleKeyword, bool>> predicate)
        {
            return Context.Set<ArticleKeyword>()
                .Where(predicate)
                .Include(x => x.Article)
                .Select(x => x.Article);
        } 
    }
}
