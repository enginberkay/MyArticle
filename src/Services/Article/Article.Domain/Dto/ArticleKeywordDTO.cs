using System;
using System.Collections.Generic;
using System.Text;

namespace Content.Domain.Dto
{
    public class ArticleKeywordDTO
    {
        public string Keyword { get; set; }

        public ArticleKeywordDTO(string keyword)
        {
            Keyword = keyword;
        }
    }
}
