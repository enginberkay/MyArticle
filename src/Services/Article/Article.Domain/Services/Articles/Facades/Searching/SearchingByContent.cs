using Content.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Content.Domain.Services.Articles.Facades.Searching
{
    public class SearchingByContent
    {
        public IUnitOfWork _unitOfWork { get; }

        public SearchingByContent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Article> SearchByContent(string payload)
        {
            return _unitOfWork.ArticleRepository.Find(x => x.Content.Contains(payload));
        }
    }
}
