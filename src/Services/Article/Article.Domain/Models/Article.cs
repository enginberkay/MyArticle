using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDraft { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<ArticleKeyword> Keywords { get; set; }

        public virtual Category Category { get; set; }

        public Article(int id, string title, string content, bool isDraft, string summary)
        {
            Id = id;
            Title = title;
            Content = content;
            IsDraft = isDraft;
            Summary = summary;
        }

        public Article() { }
    }
}
