
namespace WebsiteRank.Common.Options
{
    public class SearchProvidersSettingsOptions
    {
        public const string SECTION_NAME = "SearchProvidersSettings";
    }

    public class GoogleOptions
    {
        public const string SECTION_NAME = "Google";
        public string Url { get; set; }
        public string Identifier { get; set; }
        public int SearchResultCount { get; set; }
    }

    public class BingOptions
    {
        public const string SECTION_NAME = "Bing";
        public string Url { get; set; }
        public string Identifier { get; set; }
        public int SearchResultCount { get; set; }
    }
}
