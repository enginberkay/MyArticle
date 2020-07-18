using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles.Decarator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Content.Domain.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Save(ArticleDTO articleDto)
        {
            var article = createModel(articleDto);
            _unitOfWork.ArticleRepository.Add(article);
            _unitOfWork.Commit();
        }

        public Article createModel(ArticleDTO articleDTO)
        {
            IPersistentData persistentData = new ArticleModel();
            if (articleDTO.Category != null)
            {
                ArticleModelWithCategory modelWithCategory = new ArticleModelWithCategory(_unitOfWork, persistentData);
                return modelWithCategory.CreateModel(articleDTO);
            }
            else
                return persistentData.CreateModel(articleDTO);
        }

        //void Delete(int id);

        //void Update(int id, Article article);

        //List<Article> SearchByTitle(string title);

        //List<Article> SearchByKeyword(string keyword);

        //List<Article> Search(string payload);
    }
}
