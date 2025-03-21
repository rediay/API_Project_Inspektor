using AutoMapper;
using Common.DTO;
using Common.DTO.OwnLists;
using Common.Entities.SPsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class OwnListsResponseProfile : Profile
    {
        public OwnListsResponseProfile() : base()

        {
            CreateMap<OwnListResponseDTO, OwnListResponse>().ReverseMap();
                //.ForMember(dest => dest.OwnListTypeId, opt => opt.MapFrom(src => src.OwnListType.Id))
                //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
