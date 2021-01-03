using System.ComponentModel.DataAnnotations;

namespace WebsiteRank.Web.Models
{
    public class SearchRequestModel
    {
        [Required(ErrorMessage = "SearchPhrase is required")]
        public string SearchPhrase { get; set; }

        [Required(ErrorMessage = "WebsiteName is required")]
        public string WebsiteName { get; set; }
    }
}
