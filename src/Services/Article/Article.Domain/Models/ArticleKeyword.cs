using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Models
{
    public class ArticleKeyword
    {
        public int Id { get; set; }

        public string Keyword { get; set; }

        public Article Article { get; set; }
    }
}
