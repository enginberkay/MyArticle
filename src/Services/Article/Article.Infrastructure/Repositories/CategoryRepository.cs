using Content.Domain.Models;
using Content.Domain.Repositories;

namespace Content.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ArticleDbContext context)
            : base(context)
        { }
    }
}
