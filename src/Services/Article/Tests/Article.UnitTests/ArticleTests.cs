using System;
using Content.Domain;
using Content.Domain.Dto;
using Content.Domain.Models;
using Content.Domain.Services.Articles;
using Content.Domain.Services.Articles.Decarator;
using Content.Domain.Services.Articles.Facades.UpdatingArticle;
using Content.Domain.Services.Articles.Mapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Content.UnitTests
{
    public class ArticleTests
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly Mock<IUnitOfWork> _mockUnitofwork;
        private readonly IArticleService _articleService;

        public ArticleTests()
        {
            _mockUnitofwork = new Mock<IUnitOfWork>();
            _articleService = new ArticleService(_mockUnitofwork.Object);
            _unitofwork = _mockUnitofwork.Object;
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticleDto), MemberType = typeof(SampleDataGenerator))]
        public void MapArticle(ArticleDTO articleDTO)
        {
            var model = ArticleMapper.MapArticle(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticleDto), MemberType = typeof(SampleDataGenerator))]
        public void CreateModel(ArticleDTO articleDTO)
        {
            ArticleModel articleModel = new ArticleModel();
            var model = articleModel.CreateModel(articleDTO);
            Assert.Equal(articleDTO.Content, model.Content);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticleDtoWithCategory), MemberType = typeof(SampleDataGenerator))]
        public void CreateModelWithDecarator(ArticleDTO articleDTO, Category category)
        {
            _mockUnitofwork
                .Setup(x => x.CategoryRepository.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns((Expression<Func<Category, bool>> condition) => new List<Category>() {category});
            ArticleModel articleModel = new ArticleModel();
            ArticleModelWithCategory modelWithCategory = new ArticleModelWithCategory(_unitofwork, articleModel);
            var model = modelWithCategory.CreateModel(articleDTO);
            Assert.Equal(category, model.Category);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetArticleDTOs), MemberType = typeof(SampleDataGenerator))]
        public void SaveAnArticle(ArticleDTO articles, Category category)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.Add(It.IsAny<Article>()));
            _mockUnitofwork
                .Setup(x => x.CategoryRepository.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns((Expression<Func<Category, bool>> condition) => new List<Category>() {category});
            _articleService.Save(articles);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticle), MemberType = typeof(SampleDataGenerator))]
        public void DeleteArticle(Article article)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.Remove(It.IsAny<Article>()));
            _mockUnitofwork.Setup(x => x.ArticleRepository.GetById(It.IsAny<int>()))
                .Returns(article);
            _mockUnitofwork.Setup(x => x.ArticleKeyWordRepository.RemoveRange(
                It.IsAny<ICollection<ArticleKeyword>>()));
            _articleService.Delete(1);
        }

        [Fact]
        public void UpdatePropertyDifferences()
        {
            Article current = new Article(0, "title", "content", false, "sum");
            current.Category = new Category(0, "cat");
            current.Keywords = new List<ArticleKeyword>
            {
                new ArticleKeyword(0, "key"),
                new ArticleKeyword(0, "word")
            };

            Article input = new Article(0, "title1", "content", false, "sum");
            input.Category = new Category(0, "cat");
            input.Keywords = new List<ArticleKeyword>
            {
                new ArticleKeyword(0, "key"),
                new ArticleKeyword(0, "word1")
            };

            input.UpdatePropertyDifferences(ref current);
            Assert.Equal(input.Title, current.Title);
            Assert.Equal(input.Keywords.ElementAt(1).Keyword, input.Keywords.ElementAt(1).Keyword);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GenerateForUpdateArticleKeywords),
            MemberType = typeof(SampleDataGenerator))]
        public void UpdateArticleKeywords(Article input, Article storedArticle, List<ArticleKeyword> storedKeywords)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.GetById(It.IsAny<int>()))
                .Returns(storedArticle);
            _mockUnitofwork.Setup(x =>
                    x.ArticleKeyWordRepository.Find(It.IsAny<Expression<Func<ArticleKeyword, bool>>>()))
                .Returns(storedKeywords);
            var updatingKeyword = new UpdatingKeyword(_unitofwork);
            updatingKeyword.UpdateArticleKeywords(input);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticle), MemberType = typeof(SampleDataGenerator))]
        public void UpdateArticleBody(Article storedArticle)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.GetById(It.IsAny<int>()))
                .Returns(storedArticle);
            Article article = new Article(23, "my article11", "hello world", false, "hello");
            article.Keywords = new List<ArticleKeyword>
            {
                new ArticleKeyword(23, "key11"),
                new ArticleKeyword(24, "word")
            };
            article.Category = new Category(0, "category");

            UpdatingBody updatingBody = new UpdatingBody(_unitofwork);
            updatingBody.UpdateArticleBody(article);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticle), MemberType = typeof(SampleDataGenerator))]
        public void UpdateArticleWithNewCategory(Article storedArticle)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.GetById(It.IsAny<int>()))
                .Returns(storedArticle);
            _mockUnitofwork
                .Setup(x => x.CategoryRepository.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns(new List<Category>());
            Article article = new Article(23, "my article", "hello world", false, "hello");
            article.Category = new Category(0, Helper.RandomString(5));
            UpdatingCategory update = new UpdatingCategory(_unitofwork);
            update.UpdateArticleCategory(article);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetAnArticleWithCategory), MemberType = typeof(SampleDataGenerator))]
        public void UpdateArticleCategory(Article storedArticle, Category storedCategory)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.GetById(It.IsAny<int>()))
                .Returns(storedArticle);
            _mockUnitofwork
                .Setup(x => x.CategoryRepository.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns((Expression<Func<Category, bool>> condition) => new List<Category>() {storedCategory});
            Article article = new Article(23, "my article", "hello world", false, "hello");
            article.Category = new Category(0, "cat");
            UpdatingCategory update = new UpdatingCategory(_unitofwork);
            update.UpdateArticleCategory(article);
        }
    }
}