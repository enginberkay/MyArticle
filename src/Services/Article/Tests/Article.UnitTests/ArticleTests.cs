using Content.Domain;
using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles;
using Content.Domain.Services.Articles.Decarator;
using Content.Domain.Services.Articles.Mapper;
using Content.Infrastructure;
using Content.UnitTests;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Content.UnitTests
{
    public class ArticleTests
    {
        private readonly IUnitOfWork _unitofwork;
        //private readonly Mock<IUnitOfWork> _unitofwork;
        private readonly IArticleService _articleService;

        public ArticleTests()
        {
            var options = new DbContextOptionsBuilder<ArticleDbContext>()
                .UseNpgsql("User ID=root;Password=root;Host=localhost;Port=54320;Database=myarticle;")
                     .Options;
            ArticleDbContext context = new ArticleDbContext(options);
            _unitofwork = new UnitOfWork(context);
            //_unitofwork = new Mock<IUnitOfWork>();
            _articleService = new ArticleService(_unitofwork);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticle), MemberType = typeof(SampleDataGenerator))]
        public void MapArticle(ArticleDTO articleDTO)
        {
            var model = ArticleMapper.MapArticle(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticle), MemberType = typeof(SampleDataGenerator))]
        public void CreateModel(ArticleDTO articleDTO)
        {
            ArticleModel articleModel = new ArticleModel();
            var model = articleModel.CreateModel(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticle), MemberType = typeof(SampleDataGenerator))]
        public void CreateModelWithDecarator(ArticleDTO articleDTO)
        {
            ArticleModel articleModel = new ArticleModel();
            ArticleModelWithCategory modelWithCategory = new ArticleModelWithCategory(_unitofwork, articleModel);
            var model = modelWithCategory.CreateModel(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetArticles), MemberType = typeof(SampleDataGenerator))]
        public void SaveAnArticle(ArticleDTO articles)
        {
            _articleService.Save(articles);
        }

        [Fact]
        public void DeleteArticle()
        {
            _articleService.Delete(1);
        }
    }
}
