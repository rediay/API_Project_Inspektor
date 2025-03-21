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
    public class CompanyTypeListProfile : Profile
    {
        public CompanyTypeListProfile()

        {

            CreateMap<CompanyTypeListDTO, CompanyTypeList>()
           .ReverseMap()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ListType.Name));

        }


    }
}
