using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.Report
{
    public class QueryDetailProfile : Profile
    {
        public QueryDetailProfile()
        {
            CreateMap<QueryDetail, QueryDetailDTO>().ReverseMap();
        }
    }
}