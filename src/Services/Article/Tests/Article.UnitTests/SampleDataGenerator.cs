using Content.Domain.Dto;
using Content.Domain.Models;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Content.UnitTests
{
    public class SampleDataGenerator
    {
        public static IEnumerable<object[]> GetArticleDTOs()
        {
            yield return new object[]
            {
                new ArticleDTO("myarticle", "hello world", false, "hello", new List<ArticleKeywordDTO>
                    {
                        new ArticleKeywordDTO("key"),
                        new ArticleKeywordDTO("word")
                    }, new CategoryDTO("cat")),
            };
            yield return new object[]
            {
                new ArticleDTO("myarticle", "hello world", false, "hello", new List<ArticleKeywordDTO>
                    {
                        new ArticleKeywordDTO("key"),
                        new ArticleKeywordDTO("words")
                    }, new CategoryDTO("cate")),
            };
            yield return new object[]
            {
                new ArticleDTO("myarticle", "hello world", false, "hello", new List<ArticleKeywordDTO>
                    {
                        new ArticleKeywordDTO("key"),
                        new ArticleKeywordDTO("word")
                    }, null),
            };
        }

        public static IEnumerable<object[]> GetAnArticleDTO()
        {
            yield return new object[]
            {
                new ArticleDTO("my article", "hello world", false, "hello",
                    new List<ArticleKeywordDTO>
                    {
                        new ArticleKeywordDTO("key"),
                        new ArticleKeywordDTO("word")
                    }, new CategoryDTO("category")),
            };
        }

        public static IEnumerable<object[]> GetSomeArticles()
        {
            yield return new object[]
                {
                    new List<Article>
                    {
                        new Article{ Id = 1, Title = "my article", IsDraft = false, Summary = "this is a sample article",
                            Keywords = new List<ArticleKeyword>{
                                new ArticleKeyword(1, "key"),
                                new ArticleKeyword(2, "word")
                            },
                        Category = new Category(1, "General")},
                        new Article{ Id = 2, Title = "your article", IsDraft = false, Summary = "this is a sample article and getting longer",
                            Keywords = new List<ArticleKeyword>{
                                new ArticleKeyword(1, "find me"),
                                new ArticleKeyword(2, "other keyword")
                            },
                        Category = new Category(1, "General1")},
                        new Article{ Id = 3, Title = "your article", IsDraft = false, Summary = "this is a sample article. make some noise",
                            Keywords = new List<ArticleKeyword>{
                                new ArticleKeyword(1, "key"),
                                new ArticleKeyword(2, "word")
                            },
                            Category = new Category(1, "General2")},
                    },
                };
        }

        public static IEnumerable<object[]> GetSomeKeywords()
        {
            yield return new object[]
                {
                    new List<ArticleKeyword>
                    {
                        new ArticleKeyword
                        {
                            Id =1,
                            Keyword = "key",
                            Article = new Article{ Id = 1, Title = "my article", IsDraft = false, Summary = "this is a sample article",
                                            Keywords = new List<ArticleKeyword>{
                                                new ArticleKeyword(1, "key"),
                                                new ArticleKeyword(2, "word")
                                            },
                                            Category = new Category(1, "General")}
                        },
                        new ArticleKeyword
                        {
                            Id =2,
                            Keyword = "word",
                            Article = new Article{ Id = 1, Title = "my article", IsDraft = false, Summary = "this is a sample article",
                                            Keywords = new List<ArticleKeyword>{
                                                new ArticleKeyword(1, "key"),
                                                new ArticleKeyword(2, "word")
                                            },
                                            Category = new Category(1, "General")}
                        },
                        new ArticleKeyword
                        {
                            Id =3,
                            Keyword = "findme",
                            Article = new Article{ Id = 2, Title = "your article", IsDraft = false, Summary = "this is a sample article",
                                            Keywords = new List<ArticleKeyword>{
                                                new ArticleKeyword(3, "findme")
                                            },
                                            Category = new Category(1, "General")}
                        },
                    },
                };
        }

    }
}

