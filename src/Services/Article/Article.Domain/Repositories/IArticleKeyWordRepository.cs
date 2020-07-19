using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Content.Domain.Repositories
{
    public interface IArticleKeyWordRepository : IRepository<ArticleKeyword>
    {
        IEnumerable<Article> GetArticlesByKeyWord(Expression<Func<ArticleKeyword, bool>> predicate);
    }
}
