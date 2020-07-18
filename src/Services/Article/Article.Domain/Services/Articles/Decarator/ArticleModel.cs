using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Services.Articles.Decarator
{
    public class ArticleModel : IPersistentData
    {
        public Article CreateModel(ArticleDTO articleDto)
        {
            return ArticleMapper.MapArticle(articleDto);
        }
    }
}
