using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteRank.Data.DBContexts;
using WebsiteRank.Domain;

namespace WebsiteRank.Data.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly WebsiteRankDBContext _websiteRankDBContext;
        private readonly ILogger<SearchHistoryRepository> _logger;

        public SearchHistoryRepository(WebsiteRankDBContext websiteRankDBContext,
            ILogger<SearchHistoryRepository> logger)
        {
            _websiteRankDBContext = websiteRankDBContext;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(SearchHistory searchHistory)
        {
            var created = false;
            try
            {
                // add user in dbset and save to DB
                await _websiteRankDBContext.SearchHistory.AddAsync(searchHistory);
                 _websiteRankDBContext.SaveChanges();

                created = true;
            }
            catch (Exception ex)
            {
                // log exception
                _logger.LogError(ex.Message);
            }

            return created;
        }

        public async Task<IEnumerable<SearchHistory>> GetAsync()
        {
            return await _websiteRankDBContext.SearchHistory.ToListAsync();
               // .Where( s => s.SearchPhrase.ToUpper().Contains(searchPhrase.Trim().ToUpper())).ToListAsync();
        }
        
        public async Task<SearchHistory> GetByDayAndProviderTypeAsync(DateTime dateTime, SearchProviderType searchProviderType)
        {
            return await _websiteRankDBContext.SearchHistory
                .FirstOrDefaultAsync(s => s.LastModifiedOn.Date == dateTime.Date 
                && s.SearchProviderType.Id  == searchProviderType.Id);
                
        }


        public async Task<IEnumerable<SearchProviderType>> GetSearchProviderTypesAsync()
        {
            return await _websiteRankDBContext.SearchProviderType.ToListAsync();
        }
    }
}
