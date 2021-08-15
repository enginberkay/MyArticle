using Content.Domain.Dto;
using Content.Domain.Models;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.Extensions;

namespace Content.UnitTests
{
    public class SampleDataGenerator
    {
        private static readonly string[] Keyword = new[] {"apple", "banana", "orange", "strawberry", "kiwi"};

        private static int _keywordId = 1;

        private static Faker<ArticleKeyword> FakerArticleKeyword { get; } = new Faker<ArticleKeyword>()
            .RuleFor(o => o.Id, f => _keywordId++)
            .RuleFor(o => o.Keyword, f => f.PickRandom(Keyword));

        private static int _articleId = 1;

        private static Faker<Article> FakerArticle { get; } = new Faker<Article>().UseSeed(1338)
            .RuleFor(o => o.Id, f => _articleId++)
            .RuleFor(o => o.Content, f => f.Lorem.Paragraph(5))
            .RuleFor(o => o.Summary, f => f.Lorem.Sentences(5))
            .RuleFor(o => o.Title, f => f.Lorem.Sentence(3))
            .RuleFor(o => o.IsDraft, f => true);

        public static IEnumerable<object[]> GetArticleDTOs()
        {
            yield return new object[]
            {
                new ArticleDTO("myarticle", "hello world", false, "hello", new List<ArticleKeywordDTO>
                {
                    new ArticleKeywordDTO("key"),
                    new ArticleKeywordDTO("word")
                }, new CategoryDTO("cat")),
                new Category()
                {
                    Id = 1,
                    Name = "cat"
                }
            };
            yield return new object[]
            {
                new ArticleDTO("myarticle", "hello world", false, "hello", new List<ArticleKeywordDTO>
                {
                    new ArticleKeywordDTO("key"),
                    new ArticleKeywordDTO("words")
                }, new CategoryDTO("cate")),
                new Category()
                {
                    Id = 2,
                    Name = "cate"
                }
            };
            yield return new object[]
            {
                new ArticleDTO("myarticle", "hello world", false, "hello", new List<ArticleKeywordDTO>
                {
                    new ArticleKeywordDTO("key"),
                    new ArticleKeywordDTO("word")
                }, null),
                null
            };
        }

        public static IEnumerable<object[]> GetAnArticleDto()
        {
            yield return new object[]
            {
                new ArticleDTO("my article", "hello world", false, "hello",
                    new List<ArticleKeywordDTO>
                    {
                        new ArticleKeywordDTO("key"),
                        new ArticleKeywordDTO("word")
                    },
                    new CategoryDTO("category"))
            };
        }

        public static IEnumerable<object[]> GetAnArticleDtoWithCategory()
        {
            yield return new object[]
            {
                new ArticleDTO("my article", "hello world", false, "hello",
                    new List<ArticleKeywordDTO>
                    {
                        new ArticleKeywordDTO("key"),
                        new ArticleKeywordDTO("word")
                    },
                    new CategoryDTO("category")),
                new Category()
                {
                    Id = 1,
                    Name = "category"
                }
            };
        }

        public static IEnumerable<object[]> GetAnArticle()
        {
            yield return new object[]
            {
                new Article()
                {
                    Title = "my article", Content = "hello world", IsDraft = false, Summary = "hello",
                    Keywords = new List<ArticleKeyword>
                    {
                        new ArticleKeyword() {Id = 1, Keyword = "key"},
                        new ArticleKeyword() {Id = 2, Keyword = "word"}
                    },
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "category"
                    }
                }
            };
        }

        public static IEnumerable<object[]> GetAnArticleWithCategory()
        {
            yield return new object[]
            {
                new Article()
                {
                    Title = "my article", Content = "hello world", IsDraft = false, Summary = "hello",
                    Keywords = new List<ArticleKeyword>
                    {
                        new ArticleKeyword() {Id = 1, Keyword = "key"},
                        new ArticleKeyword() {Id = 2, Keyword = "word"}
                    },
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "category"
                    }
                },
                new Category()
                {
                    Id = 2,
                    Name = "category2"
                }
            };
        }

        public static IEnumerable<object[]> GetSomeArticles()
        {
            yield return new object[]
            {
                FakerArticle.Generate(5)
            };
        }

        public static IEnumerable<object[]> GetSomeKeywords()
        {
            yield return new object[]
            {
                FakerArticleKeyword.Generate(5)
            };
        }

        public static IEnumerable<object[]> GenerateForUpdateArticleKeywords()
        {
            yield return new object[]
            {
                new Article()
                {
                    Title = "my article", Content = "hello world", IsDraft = false, Summary = "hello",
                    Keywords = new List<ArticleKeyword>
                    {
                        new ArticleKeyword() {Id = 1, Keyword = "word"}
                    },
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "category"
                    }
                },
                new Article()
                {
                    Id = 1,
                    Title = "my article"
                },
                new List<ArticleKeyword>()
                {
                    new ArticleKeyword() {Id = 1, Keyword = "key"},
                    new ArticleKeyword() {Id = 2, Keyword = "some"},
                    new ArticleKeyword() {Id = 3, Keyword = "someone"},
                    new ArticleKeyword() {Id = 4, Keyword = "something"}
                }
            };
        }
    }
}