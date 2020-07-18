using Content.Domain;
using Content.Domain.Repositories;
using Content.Infrastructure.Repositories;
using System;
using System.Linq;

namespace Content.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleDbContext _context;
        private IArticleRepository _articleRepository;
        private IArticleKeyWordRepository _articleKeyWordRepository;
        private ICategoryRepository _categoryRepository;

        public UnitOfWork(ArticleDbContext context)
        {
            this._context = context;
        }

        public IArticleRepository ArticleRepository => _articleRepository = _articleRepository ?? new ArticleRepository(_context);

        public IArticleKeyWordRepository ArticleKeyWordRepository => _articleKeyWordRepository = _articleKeyWordRepository ?? new ArticleKeyWordRepository(_context);

        public ICategoryRepository CategoryRepository => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public int CommitAsync()
        {
            return _context.SaveChanges();
        }

        public void Rollback()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
