using Content.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Content.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();

        void Rollback();

        IArticleRepository ArticleRepository { get; }

        IArticleKeyWordRepository ArticleKeyWordRepository { get; }

        ICategoryRepository CategoryRepository { get; }
    }
}
