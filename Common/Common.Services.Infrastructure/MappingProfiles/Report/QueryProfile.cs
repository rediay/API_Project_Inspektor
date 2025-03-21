using AutoMapper;
using Common.DTO.Queries;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.Report
{
    public class QueryProfile : Profile
    {
        public QueryProfile()
        {
            CreateMap<Query, QueryDTO>().ReverseMap();
        }
    }
}