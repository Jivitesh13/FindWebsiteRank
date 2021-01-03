using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebsiteRank.Dto;

namespace WebsiteRank.SearchService.Interface
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResult>> SearchAsync(SearchRequest searchRequest);

         Task<IEnumerable<GetSearchHistoryResult>> GetSearchHistoryAsync(GetSearchHistoryRequest getSearchHistoryRequest);
    }
}
