using Content.Domain.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Content.Domain.Services.Articles.Facades.UpdatingArticle
{
    public class UpdatingCategory
    {
        public IUnitOfWork _unitOfWork { get; }

        public UpdatingCategory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateArticleCategory(Article incoming)
        {
            Article article = _unitOfWork.ArticleRepository.GetById(incoming.Id);
            if (incoming.Category == null)
            {
                article.Category = null;
                return;
            }
            if (incoming.Category?.Id != 0)
                return;
            Category category = _unitOfWork.CategoryRepository.Find(x => x.Name == incoming.Category.Name).FirstOrDefault();
            article.Category = category ?? incoming.Category;
        }
    }
}
