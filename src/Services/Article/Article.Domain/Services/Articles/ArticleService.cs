﻿using AutoMapper.Internal;
using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles.Decarator;
using Content.Domain.Services.Articles.Facades;
using Content.Domain.Services.Articles.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            persistentData = new ArticleModel();
            if (articleDTO.Category != null)
                persistentData = new ArticleModelWithCategory(_unitOfWork, persistentData);
            return persistentData.CreateModel(articleDTO);
        }

        public void Delete(int id)
        {
            Article article = _unitOfWork.ArticleRepository.GetById(id);
            _unitOfWork.ArticleKeyWordRepository.RemoveRange(article.Keywords);
            _unitOfWork.ArticleRepository.Remove(article);
            _unitOfWork.Commit();
        }

        public void Update(Article inputModel)
        {
            UpdatingArticleFacade updatingArticleFacade = new UpdatingArticleFacade(_unitOfWork);
            updatingArticleFacade.UpdateBody(inputModel);
            updatingArticleFacade.UpdateKeyword(inputModel);
            updatingArticleFacade.UpdateCategory(inputModel);
            _unitOfWork.Commit();

        }

        public List<Article> Search(string payload)
        {
            SearchingArticleFacade facade = new SearchingArticleFacade(_unitOfWork);
            return facade.Search(payload);
        }

        public List<Article> ListAll(int? limit = 15, int? start = 0)
        {
            List<Article> list = _unitOfWork.ArticleRepository.GetAll()
                .Skip(start ?? 0)
                .Take(limit ?? 15).ToList();
            list.ForEach(x =>
            {
                x.Keywords = _unitOfWork.ArticleKeyWordRepository.GetKeywordsWithoutRelated(y => y.Article == x).ToList();
                x.Category = _unitOfWork.CategoryRepository.GetCategorysWithoutRelated(y => y.Id == x.CategoryId);
            });
            return list;
        }
    }
}
