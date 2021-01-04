using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteRank.Dto;
using WebsiteRank.SearchService.Interface;
using WebsiteRank.Web.Models;

namespace WebsiteRank.Web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;

        public SearchController(ILogger<SearchController> logger,
            ISearchService searchService,
            IMapper mapper)
        {
            _logger = logger;
            _searchService = searchService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SearchRequestModel searchRequestModel)
        {
            if (searchRequestModel == null)
            {
                return BadRequest();
            }

            var result = await _searchService.SearchAsync(new SearchRequest
            {
                SearchPhrase = searchRequestModel.SearchPhrase,
                Url = searchRequestModel.WebsiteName
            });

            var response = _mapper.Map<List<SearchResultModel>>(result);
           
            return Ok(response);
        }

        [HttpGet]
        [Route("history/{url}/{top}")]
        public async Task<IActionResult> GetHistory(string url, int top)
        {
            var result = await _searchService.GetSearchHistoryAsync(new GetSearchHistoryRequest
            {
               Url = url,
               Top = top
            });

            var response = _mapper.Map<List<GetSearchHistoryResult>>(result);

            return Ok(response);
        }
    }
}