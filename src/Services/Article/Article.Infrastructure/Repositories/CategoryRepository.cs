using Content.Domain.Models;
using Content.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Content.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ArticleDbContext context)
            : base(context)
        { }

        public Category GetCategorysWithoutRelated(Expression<Func<Category, bool>> predicate)
        {
            return Context.Set<Category>().Where(predicate)
                .Select(z => new Category
                {
                    Id = z.Id,
                    Name = z.Name
                }).FirstOrDefault();
        }

    }
}
