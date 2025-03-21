/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO.Lists;
using Common.DTO.Relations_Countrys;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.MappingProfiles.Lists
{
    public class ThirdListsProfile : Profile
    {
        public ThirdListsProfile()
        {
            CreateMap<ThirdList, ThirdListsDTO>()
                .ForMember(d => d.DocumentTypename, opt=> opt.MapFrom(src => src.DocumentType.Name))
                .ReverseMap();
        }
    }
}
