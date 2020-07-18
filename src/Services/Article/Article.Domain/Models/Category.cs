using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public Category(string name)
        {
            Name = name;
        }
    }
}
