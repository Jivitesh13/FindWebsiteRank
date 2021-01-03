using System.Collections.Generic;

namespace WebsiteRank.Web.Models
{
    public class SearchHistoryResultModel
    {
        public string Provider { get; set; }
        public IEnumerable<SearchHistoryModel> Data { get; set; }
    }


    public class SearchHistoryModel
    {
        public string SearchDate { get; set; }
        public int Rank { get; set; }
    }
}
