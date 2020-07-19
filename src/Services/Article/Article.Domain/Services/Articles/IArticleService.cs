using Content.Domain.Dto;
using Content.Domain.Models;
using System.Collections.Generic;

namespace Content.Domain.Services.Articles
{
    public interface IArticleService
    {
        void Save(ArticleDTO article);

        void Delete(int id);

        void Update(Article article);

        List<Article> Search(string payload);

        List<Article> ListAll(int? limit = 15, int? start = 0);
    }
}
