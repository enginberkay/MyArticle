using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Domain.Services.Articles.Facades.Searching
{
    public class SearchingByKeyword
    {
        public IUnitOfWork _unitOfWork { get; }

        public SearchingByKeyword(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Article> SearchByKeyword(string keyword)
        {
            return _unitOfWork.ArticleKeyWordRepository.GetArticlesByKeyWord(x => x.Keyword.Contains(keyword));
        }
    }
}
