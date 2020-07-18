using AutoMapper;
using Content.Domain.Dto;
using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Services.Articles.Mapper
{
   public class CategoryMapper
    {
        public static Category MapCategory(CategoryDTO categoryDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap(
                typeof(CategoryDTO), typeof(Category)));
            var mapper = config.CreateMapper();

            Category category = mapper.Map<Category>(categoryDTO);
            return category;
        }
    }
}
