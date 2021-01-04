using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteRank.Dto;
using WebsiteRank.Web.Models;

namespace WebsiteRank.Tests.WebTests
{
    [TestClass]
    public class PostTests : SearchControllerTestsBase
    {
        SearchRequestModel _searchRequestModel;
        IList<SearchResult> _searchResult = new List<SearchResult>();
        IList<SearchResultModel> _searchResultModel = new List<SearchResultModel>();

        [TestInitialize]
        public void TestInitialize()
        {
            // call base class method to setup mocks.
            Setup();

            _searchRequestModel = new SearchRequestModel
            {
                SearchPhrase = "Land Registry Search",
                WebsiteName = "www.Infotrack.co.uk"
            };

            _searchResult.Add(new SearchResult { SearchProvider = "google", Rank = 13 });
            _searchResult.Add(new SearchResult { SearchProvider = "bing", Rank = 30 });

            _searchResult.ToList().ForEach(
                s => _searchResultModel.Add(
                    new SearchResultModel
                    {
                        SearchProvider = s.SearchProvider,
                        Rank = s.Rank
                    }));
        }

        /// <summary>
        /// Tests that bad request is return if request is null
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GivenRequestIsNullThenReturnBadRequest()
        {
            var result = await Target.Post(null) as BadRequestObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        /// <summary>
        /// Tests that result is return if request is valid
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GivenValidRequestThenReturnResult()
        {
            // arrange
            _searchService.Setup(o => o.SearchAsync(It.IsAny<SearchRequest>()))
               .Callback<SearchRequest>((r) =>
               {
                   Assert.AreEqual(_searchRequestModel.SearchPhrase, r.SearchPhrase);
                   Assert.AreEqual(_searchRequestModel.WebsiteName, r.Url);

               })
               .ReturnsAsync(_searchResult);

            _mapper.Setup(m => m.Map<List<SearchResultModel>>(It.IsAny<object>()))
                 .Returns(_searchResultModel.ToList());

            // call
            var result = await Target.Post(_searchRequestModel) as OkObjectResult;

            // assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(JsonConvert.SerializeObject(_searchResultModel), JsonConvert.SerializeObject(result.Value));

            _searchService.Verify(u => u.SearchAsync(It.IsAny<SearchRequest>()), Times.Once);
            _mapper.Verify(u => u.Map<List<SearchResultModel>>(It.IsAny<object>()), Times.Once);
        }
    }
}
