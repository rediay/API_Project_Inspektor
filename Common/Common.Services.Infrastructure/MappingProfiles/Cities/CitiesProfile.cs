/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;


using Common.DTO.Relations_Countrys;
using Common.Entities.Relations_Countrys;
using System;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class CitiesProfile : Profile
    {
        public CitiesProfile()

        {
            CreateMap<Entities.Relations_Countrys.Cities, CitiesDTO>().ReverseMap().
                ForMember(d => d.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
