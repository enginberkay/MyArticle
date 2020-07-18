using System;
using System.Collections.Generic;
using System.Text;

namespace Article.Domain.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDraft { get; set; }

        public string Summary { get; set; }

        public ICollection<ArticleKeyword> Keywords { get; set; }

        public Category Category { get; set; }
    }
}
