﻿using AutoMapper;
using WebsiteRank.Dto;
using WebsiteRank.Web.Models;

namespace WebsiteRank.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SearchResult, SearchResultModel>();

            CreateMap<SearchHistoryResult, SearchHistoryModel>();

            CreateMap<GetSearchHistoryResult, SearchHistoryResultModel>();
        }
    }
}
