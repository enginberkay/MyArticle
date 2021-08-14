using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Dto
{
    public class CategoryDTO
    {
        public string Name { get; set; }

        public CategoryDTO(string name)
        {
            Name = name;
        }
    }
}
