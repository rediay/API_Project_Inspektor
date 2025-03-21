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
    public class PermissionsProfile : Profile
    {
        public PermissionsProfile()

        {

            CreateMap<Permissions,PermissionsDTO>()           
           .ForMember(d => d.Status, src => src.MapFrom(d => d.Status))
           .ForMember(d => d.Children , src => src.MapFrom(d => d.SubModule))
           .ReverseMap();
            CreateMap<Modules, ModulesDTO>()                           
           .ForMember(d => d.Title, src => src.MapFrom(d => d.Title))
           .ForMember(d => d.icon, src => src.MapFrom(d => d.icon))
           .ForMember(d => d.Children, src => src.MapFrom(d => d.SubModules))
           .ReverseMap();
            CreateMap<SubModules, Children>()
           .ReverseMap()
           .ForMember(d => d.Title, src => src.MapFrom(d => d.Title))
            .ForMember(d => d.icon, src => src.MapFrom(d => d.icon))
            .ForMember(d => d.link, src => src.MapFrom(d => d.link));
            CreateMap<Permissions, PermissionsRequest>()
                .ReverseMap();
        }


    }
}
