using Content.Domain.Dto;
using Content.Domain.Models;

namespace Content.Domain.Services.Articles
{
    public interface IArticleService
    {
        void Save(ArticleDTO article);

        void Delete(int id);

        void Update(Article article);

        //List<Article> SearchByTitle(string title);

        //List<Article> SearchByKeyword(string keyword);

        //List<Article> Search(string payload);
    }
}
