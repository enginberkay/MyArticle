using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Content.Domain.Services.Articles.Facades.Searching
{
    public class SearchingByTitle
    {
        public IUnitOfWork _unitOfWork { get; }

        public SearchingByTitle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Article> SearchByTitle(string title)
        {
            return _unitOfWork.ArticleRepository.Find(x => x.Title.Contains(title));
        }
    }
}
