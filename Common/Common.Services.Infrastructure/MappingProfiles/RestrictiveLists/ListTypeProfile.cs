using AutoMapper;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.RestrictiveLists
{
    public class ListTypeProfile : Profile
    {
        public ListTypeProfile()
        {
            //CreateMap<ListType, ListTypeDTO>().ReverseMap();
            CreateMap<ListType, ListTypeDTO>()
                .ForMember(d => d.Periodicity, opt
                    => opt.MapFrom(src => src.Periodicity)
                )
                .ReverseMap();
        }
    }
}