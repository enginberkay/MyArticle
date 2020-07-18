using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Content.Domain.Services.Articles.Decarator
{
    public class ArticleModelWithCategory : IPersistentData
    {
        private IUnitOfWork _unitOfWork { get; }
        public IPersistentData _data { get; }

        public ArticleModelWithCategory(IUnitOfWork unitOfWork, IPersistentData data)
        {
            _unitOfWork = unitOfWork;
            _data = data;
        }

        public Article CreateModel(ArticleDTO articleDto)
        {
            var article = _data.CreateModel(articleDto);
            Category categories = _unitOfWork.CategoryRepository.Find(q => q.Name == articleDto.Category.Name).ToList().FirstOrDefault();
            article.Category = categories ?? CategoryMapper.MapCategory(articleDto.Category);
            return article;
        }

    }
}
