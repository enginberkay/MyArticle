using System;
using System.Collections.Generic;
using System.Text;

namespace Article.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
