using System.Collections.Generic;

namespace WebsiteRank.Web.Models
{
    public class SearchHistoryResultModel
    {
        public string Provider { get; set; }
        public IList<SearchHistoryModel> Data { get; } = new List<SearchHistoryModel>();
    }


    public class SearchHistoryModel
    {
        public string SearchDate { get; set; }
        public int Rank { get; set; }
    }
}
