using Content.Domain.Dto;
using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Services.Articles.Decarator
{
    public interface IPersistentData
    {
        Article CreateModel(ArticleDTO articleDTO);
    }
}
