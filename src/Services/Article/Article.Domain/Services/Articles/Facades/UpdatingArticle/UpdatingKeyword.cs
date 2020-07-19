using AutoMapper.Internal;
using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Content.Domain.Services.Articles.Facades.UpdatingArticle
{
    public class UpdatingKeyword
    {
        public IUnitOfWork _unitOfWork { get; }


        public UpdatingKeyword(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateArticleKeywords(Article incoming)
        {
            List<ArticleKeyword> currentKeywords = _unitOfWork.ArticleKeyWordRepository.Find(x => x.Article == incoming).ToList();

            currentKeywords.Select(x =>
            {
                ArticleKeyword inKey = getItemFrom(x.Id, incoming.Keywords);
                if (inKey.Keyword != x.Keyword)
                    x.Keyword = inKey.Keyword;
                return x;
            }).ToList();
            var newRecord = incoming.Keywords.Where(x => x.Id == 0).ToList();
            if (newRecord.Count > 0)
            {
                var article = _unitOfWork.ArticleRepository.GetById(incoming.Id);
                newRecord.Where(x => x.Article == null).Select(x => x.Article = article).ToList();
                _unitOfWork.ArticleKeyWordRepository.AddRange(newRecord);
            }
        }

        ArticleKeyword getItemFrom(int index, ICollection<ArticleKeyword> articles)
        {
            return articles.Where(x => x.Id == index).FirstOrDefault();
        }
    }
}
