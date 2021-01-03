using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebsiteRank.Common.Options;
using WebsiteRank.SearchProvider.Interface;


namespace WebsiteRank.SearchProvider.Implementation
{
    public class GoogleService : IProviderService
    {
        private readonly GoogleOptions _googleOptions;
        private readonly ILogger<GoogleService> _logger;

        public GoogleService(ILogger<GoogleService> logger, IOptions<GoogleOptions> googleOptions)
        {
            _logger = logger;
            _googleOptions = googleOptions.Value;
        }
        public ProviderType Type => ProviderType.Google;

        public async Task<int> SearchAsync(string searchPhrase, string url)
        {
            string webResponseString;

            // search
            var query = new Dictionary<string, string>
            {
                ["q"] = searchPhrase,
                ["num"] = _googleOptions.SearchResultCount.ToString(),
                // ...
            };

            var searchUrl = QueryHelpers.AddQueryString(_googleOptions.Url, query);

            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.Default;
                webResponseString = await wc.DownloadStringTaskAsync(new Uri(searchUrl));
            }

            // find rank

            var matches = Regex.Matches(webResponseString, _googleOptions.Identifier)
                            .Cast<Match>().ToList();

            var indexes = matches.Select((x, i) => new { i, x })
                .Where(x => x.ToString().Contains(url))
                .Select(x => x.i + 1)
                .ToList();

            return indexes.FirstOrDefault();
        }
    }
}
