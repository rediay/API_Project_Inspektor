using AutoMapper;
using Common.DTO;
using Common.DTO.OwnLists;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class OwnListsProfile:Profile
    {
        public OwnListsProfile() : base()

        {
            CreateMap< OwnListDTO, OwnList>().ReverseMap()
                .ForMember(dest => dest.OwnListTypeId, opt => opt.MapFrom(src => src.OwnListType.Id))
                .ForMember(dest => dest.OwnListTypeName, opt => opt.MapFrom(src => src.OwnListType.Name))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
