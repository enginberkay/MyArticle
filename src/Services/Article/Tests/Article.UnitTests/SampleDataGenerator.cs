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
    }
}

