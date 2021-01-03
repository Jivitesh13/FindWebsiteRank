using System.Threading.Tasks;

namespace WebsiteRank.SearchProvider.Interface
{
    public interface IProviderService
    {
        ProviderType Type { get; }
        Task<int> SearchAsync(string searchPhrase, string url);
    }
}
