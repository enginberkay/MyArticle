using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Domain.Dto;
using Content.Domain.Services.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Content.API.Controllers
{
    public class ArticleController : ControllerBase
    {
        public IArticleService _articleService { get; }

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        
        [HttpPost]
        public IActionResult SaveAnArticle([FromBody] ArticleDTO articleDTO)
        {
            _articleService.Save(articleDTO);
            return Accepted();
        }

        [HttpDelete]
        public IActionResult DeleteArticle(int id)
        {
            _articleService.Delete(id);
            return Ok();
        }
    }
}
