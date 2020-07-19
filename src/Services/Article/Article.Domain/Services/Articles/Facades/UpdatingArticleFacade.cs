using Content.Domain.Models;
using Content.Domain.Services.Articles.Facades.UpdatingArticle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Services.Articles.Facades
{
    public class UpdatingArticleFacade
    {
        UpdatingBody _updatingBody;
        UpdatingKeyword _updatingKeyword;
        UpdatingCategory _updatingCategory;

        public UpdatingArticleFacade(IUnitOfWork unitOfWork)
        {
            _updatingBody = new UpdatingBody(unitOfWork);
            _updatingKeyword = new UpdatingKeyword(unitOfWork);
            _updatingCategory = new UpdatingCategory(unitOfWork);
        }

        public void UpdateBody(Article article)
        {
            _updatingBody.UpdateArticleBody(article);
        }

        public void UpdateKeyword(Article article)
        {
            _updatingKeyword.UpdateArticleKeywords(article);
        }

        public void UpdateCategory(Article article)
        {
            _updatingCategory.UpdateArticleCategory(article);
        }
    }
}
