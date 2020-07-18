using Content.Domain.Repositories;
using System;

namespace Content.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        int CommitAsync();

        IArticleRepository ArticleRepository { get; }

        IArticleKeyWordRepository ArticleKeyWordRepository { get; }

        ICategoryRepository CategoryRepository { get; }
    }
}
