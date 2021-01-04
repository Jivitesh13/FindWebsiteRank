using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteRank.Data.Repositories;
using WebsiteRank.Domain;
using WebsiteRank.Dto;
using WebsiteRank.SearchProvider.Interface;
using WebsiteRank.SearchService.Interface;

namespace WebsiteRank.SearchService.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> _logger;
        private readonly IEnumerable<IProviderService> _providerServices;
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public SearchService(ILogger<SearchService> logger,
            IEnumerable<IProviderService> providerServices,
            ISearchHistoryRepository searchHistoryRepository)
        {
            _logger = logger;
            _providerServices = providerServices;
            _searchHistoryRepository = searchHistoryRepository;
        }

        /// <summary>
        /// Perfomrs web search
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchResult>> SearchAsync(SearchRequest searchRequest)
        {  
            var searchResultList = new List<SearchResult>();

            var providers = await _searchHistoryRepository.GetSearchProviderTypesAsync();

            foreach (var providerService in _providerServices)
            {
                var provider = providers.FirstOrDefault(p => p.Id == (int)providerService.Type);

                if (provider != null)
                {
                    var rank = 0;

                    // use search result if already exist for today
                    var searchResultToday = await _searchHistoryRepository.GetByDayAndProviderTypeAsync(DateTime.Today, provider);

                    if (searchResultToday != null)
                    {
                        rank = searchResultToday.Rank;
                    }
                    else
                    {
                        // start new Search using provider i.e google
                        rank = await providerService.SearchAsync(searchRequest.SearchPhrase, searchRequest.Url);

                        // Save in Db
                        await _searchHistoryRepository.CreateAsync(new SearchHistory
                        {
                            Rank = rank,
                            SearchPhrase = searchRequest.SearchPhrase,
                            UrlToSearchInResult = searchRequest.Url,
                            LastModifiedOn = DateTime.Now,
                            SearchProviderTypeId = provider.Id
                        });
                    }

                    // Add in result
                    searchResultList.Add(new SearchResult
                    {
                        Rank = rank,
                        SearchProvider = provider.Description
                    });
                }
            }

            return searchResultList;
        }

        /// <summary>
        /// Retrieves search history
        /// </summary>
        /// <param name="getSearchHistoryRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetSearchHistoryResult>> GetSearchHistoryAsync(GetSearchHistoryRequest getSearchHistoryRequest)
        {
            var getSearchHistoryResultList = new List<GetSearchHistoryResult>();

            // get search history from DB
            var searchHistory = await _searchHistoryRepository.GetAsync();

            // get list of providers
            var providers = await _searchHistoryRepository.GetSearchProviderTypesAsync();

            foreach (var provider in providers)
            {
                // filter search history
                var searchHistoryForProvider = searchHistory.ToList()
                    .Where(s => s.SearchProviderType.Id == provider.Id
                            && s.UrlToSearchInResult.Contains(getSearchHistoryRequest.Url))
                    .OrderByDescending(s => s.LastModifiedOn)
                    .Take(getSearchHistoryRequest.Top)
                    .OrderBy(s => s.LastModifiedOn)
                    .ToList();

                if (searchHistoryForProvider != null && searchHistoryForProvider.Count > 0)
                {
                    var getSearchHistoryResult = new GetSearchHistoryResult { Provider = provider.Name };

                    searchHistoryForProvider.ForEach(s =>
                                getSearchHistoryResult.Data
                                .Add(new SearchHistoryResult
                                {
                                    SearchDate = s.LastModifiedOn.ToShortDateString(),
                                    Rank = s.Rank
                                }));

                    getSearchHistoryResultList.Add(getSearchHistoryResult);

                }
            }

            return getSearchHistoryResultList;

        }
    }
}
