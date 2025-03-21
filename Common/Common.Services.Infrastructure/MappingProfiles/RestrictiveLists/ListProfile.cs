using AutoMapper;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.RestrictiveLists
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<List, ListDTO>().ReverseMap();

            CreateMap<List, ListDTO>()
             .ForMember(d => d.ThirdListDocument, opt => opt.MapFrom(src => src.ThirdList.Document))
             .ForMember(d => d.ThirdListName, opt => opt.MapFrom(src => src.ThirdList.Name))
             .ReverseMap();
        }
    }
}