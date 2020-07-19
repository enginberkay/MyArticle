using Content.Domain.Models;
using Content.Domain.Services.Articles.Facades.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Content.Domain.Services.Articles.Facades
{
    public class SearchingArticleFacade
    {
        public IUnitOfWork UnitOfWork { get; }
        private SearchingByTitle _searchingByTitle;
        private SearchingByKeyword _searchingByKeyword;
        private SearchingByContent _searchingByContent;

        public SearchingArticleFacade(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _searchingByTitle = new SearchingByTitle(unitOfWork);
            _searchingByKeyword = new SearchingByKeyword(unitOfWork);
            _searchingByContent = new SearchingByContent(unitOfWork);
        }

        public IEnumerable<Article> SearchByTitle(string title)
        {
            return _searchingByTitle.SearchByTitle(title);
        }

        public IEnumerable<Article> SearchByKeyword(string keyword)
        {
            return _searchingByKeyword.SearchByKeyword(keyword);
        }

        public IEnumerable<Article> SearchByContent(string payload)
        {
            return _searchingByContent.SearchByContent(payload);
        }

        public List<Article> Search(string payload)
        {
            var titles = _searchingByTitle.SearchByTitle(payload);
            var keywords = _searchingByKeyword.SearchByKeyword(payload);
            var contents = _searchingByContent.SearchByContent(payload);
            IEnumerable<Article> total;
            total = titles.Concat(keywords);
            total = total.Concat(contents);
            total.Distinct();
            return total.ToList();
        }

    }
}
