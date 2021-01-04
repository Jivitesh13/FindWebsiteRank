using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteRank.Dto;

namespace WebsiteRank.Tests.WebTests
{
    [TestClass]
    public class GetHistoryTests : SearchControllerTestsBase
    {
        GetSearchHistoryRequest _getSearchHistoryRequest;
        IList<GetSearchHistoryResult> _getSearchHistoryResultList = new List<GetSearchHistoryResult>();
       
        [TestInitialize]
        public void TestInitialize()
        {
            // call base class method to setup mocks.
            Setup();

            _getSearchHistoryRequest = new GetSearchHistoryRequest
            {
                Url = "www.infortrack.co.uk",
                Top = 30
            };

            var getSearchHistoryResult = new GetSearchHistoryResult
            {
                Provider = "google"
            };

            getSearchHistoryResult.Data.Add(
                new SearchHistoryResult { SearchDate = "02/01/2021", Rank = 20 });

            getSearchHistoryResult.Data.Add(
                new SearchHistoryResult { SearchDate = "03/01/2021", Rank = 15 });

            _getSearchHistoryResultList.Add(getSearchHistoryResult);
        }

        /// <summary>
        /// Tests that result is return if request is valid
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GivenValidRequestThenReturnResult()
        {
            // arrange
            _searchService.Setup(o => o.GetSearchHistoryAsync(It.IsAny<GetSearchHistoryRequest>()))
               .Callback<GetSearchHistoryRequest>((r) =>
               {
                   Assert.AreEqual(_getSearchHistoryRequest.Url, r.Url);
                   Assert.AreEqual(_getSearchHistoryRequest.Top, r.Top);

               })
               .ReturnsAsync(_getSearchHistoryResultList);

            _mapper.Setup(m => m.Map<List<GetSearchHistoryResult>>(It.IsAny<object>()))
                 .Returns(_getSearchHistoryResultList.ToList());

            // call
            var result = await Target.GetHistory(_getSearchHistoryRequest.Url, _getSearchHistoryRequest.Top) 
                as OkObjectResult;

            // assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(JsonConvert.SerializeObject(_getSearchHistoryResultList), JsonConvert.SerializeObject(result.Value));

            _searchService.Verify(u => u.GetSearchHistoryAsync(It.IsAny<GetSearchHistoryRequest>()), Times.Once);
            _mapper.Verify(u => u.Map<List<GetSearchHistoryResult>>(It.IsAny<object>()), Times.Once);
        }
    }
}
