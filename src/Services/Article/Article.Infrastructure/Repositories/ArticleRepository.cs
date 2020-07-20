using Content.Domain.Models;
using Content.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Content.Infrastructure.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(ArticleDbContext context)
            : base(context)
        { }

        public new Article GetById(int id)
        {
            return Context.Set<Article>()
                .Include(x => x.Category)
                .Include(x => x.Keywords)
                .FirstOrDefault(x => x.Id == id);
        }

        public new IEnumerable<Article> Find(Expression<Func<Article, bool>> predicate)
        {
            return Context.Set<Article>()
                .Include(x => x.Category)
                .Include(x => x.Keywords)
                .Where(predicate);
        }
    }
}
