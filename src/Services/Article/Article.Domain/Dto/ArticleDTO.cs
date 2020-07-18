using Content.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Dto
{
    public class ArticleDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDraft { get; set; }

        public string Summary { get; set; }

        public List<ArticleKeywordDTO> Keywords { get; set; }

        public CategoryDTO Category { get; set; }

        public ArticleDTO()
        {

        }
        public ArticleDTO(string title, string content, bool isDraft, string summary, List<ArticleKeywordDTO> articleKeywords, CategoryDTO category)
        {
            Title = title;
            Content = content;
            IsDraft = isDraft;
            Summary = summary;
            Keywords = articleKeywords;
            Category = category;
        }
    }
}
