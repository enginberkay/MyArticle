using AutoMapper;
using Content.Domain.Dto;
using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Services.Articles.Mapper
{
    public class ArticleMapper
    {
        public static Article MapArticle(ArticleDTO model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArticleDTO, Article>();
                cfg.CreateMap<ArticleKeywordDTO, ArticleKeyword>();
                cfg.CreateMap<CategoryDTO, Category>();
            });
            var mapper = config.CreateMapper();

            Article article = mapper.Map<Article>(model);
            return article;
        }
    }
}
