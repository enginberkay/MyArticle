using Content.Domain.Models;
using Content.Domain.Repositories;

namespace Content.Infrastructure.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(ArticleDbContext context)
            : base(context)
        { }
    }
}
