using System.Collections.Generic;

namespace WebsiteRank.Dto
{
    public class GetSearchHistoryResult
    {
        public string Provider { get; set; }

        public List<SearchHistoryResult> Data { get; } = new List<SearchHistoryResult>();
    }

    public class SearchHistoryResult
    {
        public string SearchDate { get; set; }
        public int Rank { get; set; }
    }
}
