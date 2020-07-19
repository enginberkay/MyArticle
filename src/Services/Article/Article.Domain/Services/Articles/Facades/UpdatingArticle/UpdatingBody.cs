using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Services.Articles.Facades.UpdatingArticle
{
    public class UpdatingBody
    {
        public IUnitOfWork _unitOfWork { get; }

        public UpdatingBody(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void UpdateArticleBody(Article incoming)
        {
            Article current = _unitOfWork.ArticleRepository.GetById(incoming.Id);
            incoming.UpdatePropertyDifferences(ref current);
        }
    }
}
