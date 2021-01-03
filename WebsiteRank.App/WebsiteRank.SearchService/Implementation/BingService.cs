using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebsiteRank.Common.Options;
using WebsiteRank.SearchProvider.Interface;

namespace WebsiteRank.SearchProvider.Implementation
{
    public class BingService : IProviderService
    {
        private readonly BingOptions _bingOptions;
        private readonly ILogger<BingService> _logger;

        public BingService(ILogger<BingService> logger, IOptions<BingOptions> bingOptions)
        {
            _logger = logger;
            _bingOptions = bingOptions.Value;
        }

        public ProviderType Type => ProviderType.Bing;

        public async Task<int> SearchAsync(string searchPhrase, string url)
        {
            //TODO : implement bing scrape
            return await Task.Run(() => new Random().Next(0, 100));
        }
    }
}
