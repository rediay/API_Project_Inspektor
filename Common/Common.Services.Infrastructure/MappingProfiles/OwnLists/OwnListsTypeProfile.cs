using AutoMapper;
using Common.DTO;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class OwnListsTypeProfile : Profile
    {
        public OwnListsTypeProfile() : base()

        {
            CreateMap<OwnListTypesDTO, OwnListType>().ReverseMap();
                 
        }
    }
}
