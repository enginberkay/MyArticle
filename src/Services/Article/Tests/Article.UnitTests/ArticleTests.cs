using Content.Domain;
using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles;
using Content.Domain.Services.Articles.Decarator;
using Content.Domain.Services.Articles.Facades.UpdatingArticle;
using Content.Domain.Services.Articles.Mapper;
using Content.Infrastructure;
using Content.UnitTests;
using Microsoft.AspNetCore.Localization;
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
        [MemberData(nameof(SampleDataGenerator.GetAnArticleDTO), MemberType = typeof(SampleDataGenerator))]
        public void MapArticle(ArticleDTO articleDTO)
        {
            var model = ArticleMapper.MapArticle(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticleDTO), MemberType = typeof(SampleDataGenerator))]
        public void CreateModel(ArticleDTO articleDTO)
        {
            ArticleModel articleModel = new ArticleModel();
            var model = articleModel.CreateModel(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticleDTO), MemberType = typeof(SampleDataGenerator))]
        public void CreateModelWithDecarator(ArticleDTO articleDTO)
        {
            ArticleModel articleModel = new ArticleModel();
            ArticleModelWithCategory modelWithCategory = new ArticleModelWithCategory(_unitofwork, articleModel);
            var model = modelWithCategory.CreateModel(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetArticleDTOs), MemberType = typeof(SampleDataGenerator))]
        public void SaveAnArticle(ArticleDTO articles)
        {
            _articleService.Save(articles);
        }

        [Fact]
        public void DeleteArticle()
        {
            _articleService.Delete(1);
        }

        [Fact]
        public void UpdatePropertyDifferences()
        {
            Article current = new Article(0, "title", "content", false, "sum");
            current.Category = new Category(0,"cat");
            current.Keywords = new List<ArticleKeyword>
            {
                new ArticleKeyword(0, "key"),
                new ArticleKeyword(0, "word")
            };

            Article input = new Article(0, "title1", "content", false, "sum");
            input.Category = new Category(0,"cat");
            input.Keywords = new List<ArticleKeyword>
            {
                new ArticleKeyword(0, "key"),
                new ArticleKeyword(0, "word1")
            };

            input.UpdatePropertyDifferences(ref current);
            Assert.Equal(input.Title, current.Title);
            Assert.Equal(input.Keywords.ElementAt(1).Keyword, input.Keywords.ElementAt(1).Keyword);

        }

        [Fact]
        public void UpdateArticleKeywords()
        {
            Article article = new Article(23, "my article", "hello world", false, "hello");
            article.Keywords = new List<ArticleKeyword>
                    {
                        new ArticleKeyword(23,"key11"),
                        new ArticleKeyword(24,"word")
                    };
            article.Category = new Category(0,"category");

            UpdatingKeyword updatingKeyword = new UpdatingKeyword(_unitofwork);
            updatingKeyword.UpdateArticleKeywords(article);
            _unitofwork.Commit();
        }

        [Fact]
        public void UpdateArticleBody()
        {
            Article article = new Article(23, "my article11", "hello world", false, "hello");
            article.Keywords = new List<ArticleKeyword>
                    {
                        new ArticleKeyword(23,"key11"),
                        new ArticleKeyword(24,"word")
                    };
            article.Category = new Category(0,"category");

            UpdatingBody updatingBody = new UpdatingBody(_unitofwork);
            updatingBody.UpdateArticleBody(article);
            _unitofwork.Commit();
        }

        [Fact]
        public void UpdateArticleWithNewCategory()
        {
            Article article = new Article(23, "my article", "hello world", false, "hello");
            article.Category = new Category(0, Helper.RandomString(5));
            UpdatingCategory update = new UpdatingCategory(_unitofwork);
            update.UpdateArticleCategory(article);
            _unitofwork.Commit();
        }

        [Fact]
        public void UpdateArticleCategory()
        {
            Article article = new Article(23, "my article", "hello world", false, "hello");
            article.Category = new Category(0, "cat");
            UpdatingCategory update = new UpdatingCategory(_unitofwork);
            update.UpdateArticleCategory(article);
            _unitofwork.Commit();
        }
    }
}
