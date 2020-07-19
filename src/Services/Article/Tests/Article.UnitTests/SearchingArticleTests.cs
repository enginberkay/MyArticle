using Content.Domain;
using Content.Domain.Models;
using Content.Domain.Services.Articles;
using Content.Domain.Services.Articles.Facades.Searching;
using Content.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace Content.UnitTests
{
    public class SearchingArticleTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitofwork;
        private readonly IArticleService _articleService;

        public SearchingArticleTests()
        {
            _mockUnitofwork = new Mock<IUnitOfWork>();
            _articleService = new ArticleService(_mockUnitofwork.Object);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetSomeArticles), MemberType = typeof(SampleDataGenerator))]
        public void SearchByTitle(List<Article> articles)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.Find(It.IsAny<Expression<Func<Article, bool>>>()))
                .Returns(
                (Expression<Func<Article, bool>> condition) =>
                articles.AsQueryable().Where(condition)
                );
            SearchingByTitle searching = new SearchingByTitle(_mockUnitofwork.Object);
            var result = searching.SearchByTitle("my article");
            Assert.Single(result);
            _mockUnitofwork.Verify(x => x.ArticleRepository.Find(It.IsAny<Expression<Func<Article, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetSomeArticles), MemberType = typeof(SampleDataGenerator))]
        public void SearchByContent(List<Article> articles)
        {
            _mockUnitofwork.Setup(x => x.ArticleRepository.Find(It.IsAny<Expression<Func<Article, bool>>>()))
                .Returns(
                (Expression<Func<Article, bool>> condition) =>
                articles.AsQueryable().Where(condition)
                );
            SearchingByContent searching = new SearchingByContent(_mockUnitofwork.Object);
            var result = searching.SearchByContent("awesome");
            Assert.NotEmpty(result);
            _mockUnitofwork.Verify(x => x.ArticleRepository.Find(It.IsAny<Expression<Func<Article, bool>>>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(SampleDataGenerator.GetSomeKeywords), MemberType = typeof(SampleDataGenerator))]
        public void SearchByKeyword(List<ArticleKeyword> articles)
        {
            _mockUnitofwork.Setup(x => x.ArticleKeyWordRepository.GetArticlesByKeyWord(It.IsAny<Expression<Func<ArticleKeyword, bool>>>()))
                .Returns(
                (Expression<Func<ArticleKeyword, bool>> condition) =>
                articles.AsQueryable().Where(condition).Select(x => x.Article)
                );
            SearchingByKeyword searching = new SearchingByKeyword(_mockUnitofwork.Object);
            var result = searching.SearchByKeyword("findme");
            Assert.NotEmpty(result);
            _mockUnitofwork.Verify(x => x.ArticleKeyWordRepository.GetArticlesByKeyWord(It.IsAny<Expression<Func<ArticleKeyword, bool>>>()), Times.Once);
        }
    }
}
