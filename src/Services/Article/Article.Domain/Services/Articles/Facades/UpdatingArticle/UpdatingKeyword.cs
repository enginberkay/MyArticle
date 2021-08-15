using AutoMapper.Internal;
using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
            List<ArticleKeyword> storedKeywords =
                _unitOfWork.ArticleKeyWordRepository.Find(x => x.Article == incoming).ToList();

            var currentKeywords = storedKeywords.Select(x =>
            {
                ArticleKeyword inKey = GetItemFrom(x.Id, incoming.Keywords);
                if (inKey != null && inKey.Keyword != x.Keyword)
                    x.Keyword = inKey.Keyword;
                return x;
            }).ToList();
            var newRecords = incoming.Keywords.Where(x => !currentKeywords.Contains(x)).ToList();
            if (newRecords.Count <= 0) return;

            var article = _unitOfWork.ArticleRepository.GetById(incoming.Id);
            newRecords = newRecords.Where(x => x.Article == null)
                .Select(x =>
                {
                    x.Article = article;
                    return x;
                }).ToList();
            _unitOfWork.ArticleKeyWordRepository.AddRange(newRecords);
        }

        private static ArticleKeyword GetItemFrom(int index, IEnumerable<ArticleKeyword> articles)
        {
            return articles.FirstOrDefault(x => x.Id == index);
        }
    }
}