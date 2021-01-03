
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteRank.Domain
{
    [Table("SearchHistory")]
    public class SearchHistory
    {
        [Key]
        public int Id { get; set; }

        public int SearchProviderTypeId { get; set; }

        public string SearchPhrase { get; set; }

        public string UrlToSearchInResult { get; set; }

        public int Rank { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public virtual SearchProviderType SearchProviderType { get; set; }
    }
}
