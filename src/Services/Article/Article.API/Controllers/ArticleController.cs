using System.Collections.Generic;
using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles;
using Microsoft.AspNetCore.Mvc;

namespace Content.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPut]
        public IActionResult UpdateArticle([FromBody] Article articleDTO)
        {
            _articleService.Update(articleDTO);
            return Ok();
        }

        [HttpGet("Search")]
        public IActionResult Search(string payload)
        {
            var model = _articleService.Search(payload);
            return Ok(model);
        }

        [HttpGet("List")]
        public IActionResult List(int? limit, int? start)
        {
            List<Article> articles = _articleService.ListAll(limit, start);
            return Ok(articles);
        }
    }
}