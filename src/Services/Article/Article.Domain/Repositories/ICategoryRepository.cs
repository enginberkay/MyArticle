using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Content.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategorysWithoutRelated(Expression<Func<Category, bool>> predicate);
    }
}
