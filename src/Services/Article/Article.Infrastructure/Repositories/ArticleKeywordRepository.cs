using Content.Domain.Models;
using Content.Domain.Repositories;

namespace Content.Infrastructure.Repositories
{
    public class ArticleKeyWordRepository : Repository<ArticleKeyword>, IArticleKeyWordRepository
    {
        public ArticleKeyWordRepository(ArticleDbContext context)
            : base(context)
        { }
    }
}
