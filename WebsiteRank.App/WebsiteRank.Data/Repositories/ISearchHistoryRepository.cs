using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebsiteRank.Domain;

namespace WebsiteRank.Data.Repositories
{
    public interface ISearchHistoryRepository
    {
        Task<bool> CreateAsync(SearchHistory user);
        Task<IEnumerable<SearchHistory>> GetAsync();
        Task<SearchHistory> GetByDayAndProviderTypeAsync(DateTime dateTime, SearchProviderType searchProviderType);
        Task<IEnumerable<SearchProviderType>> GetSearchProviderTypesAsync();
    }
}
