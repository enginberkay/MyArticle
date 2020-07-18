using System;
using System.Collections.Generic;
using System.Text;

namespace Article.Domain.Models
{
    public class ArticleKeyword
    {
        public int Id { get; set; }

        public string Keyword { get; set; }

        public Article Article { get; set; }
    }
}
