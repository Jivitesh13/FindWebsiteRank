using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using WebsiteRank.SearchService.Interface;
using WebsiteRank.Web.Controllers;

namespace WebsiteRank.Tests.WebTests
{
    public class SearchControllerTestsBase
    {
        protected Mock<ILogger<SearchController>> _logger;
        protected Mock<ISearchService> _searchService;
        protected Mock<IMapper> _mapper;

        public SearchController Target;
        protected void Setup()
        {
            _logger = new Mock<ILogger<SearchController>>(MockBehavior.Strict);
            _searchService = new Mock<ISearchService>(MockBehavior.Strict);
            _mapper = new Mock<IMapper>(MockBehavior.Strict);

            Target = new SearchController(_logger.Object, _searchService.Object, _mapper.Object);
        }
    }
}
