using AutoMapper;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.SPsData;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class QueryListProfile : Profile
    {
        public QueryListProfile()
        {
            CreateMap<ListResponse, ListDTO>().ReverseMap();
        }
    }
}