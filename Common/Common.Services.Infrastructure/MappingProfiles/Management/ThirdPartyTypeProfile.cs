/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class ThirdPartyTypeProfile : Profile
    {
        public ThirdPartyTypeProfile() : base()

        {
            CreateMap<ThirdPartyTypeDTO, ThirdPartyType>().ReverseMap()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id)); 
        }


    }
}
