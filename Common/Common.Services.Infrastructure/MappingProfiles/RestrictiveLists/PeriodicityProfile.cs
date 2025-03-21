using AutoMapper;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.RestrictiveLists
{
    public class PeriodicityProfile : Profile
    {
        public PeriodicityProfile()
        {
            //CreateMap<ListType, ListTypeDTO>().ReverseMap();
            CreateMap<Periodicity, PeriodicityDTO>()
                .ReverseMap();
        }
    }
}