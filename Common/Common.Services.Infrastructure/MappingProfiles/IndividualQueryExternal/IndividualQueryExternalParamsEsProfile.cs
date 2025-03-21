using AutoMapper;
using Common.DTO.IndividualQueryExternal;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.MappingProfiles.IndividualQueryExternal
{
    public class IndividualQueryExternalParamsEsProfile : Profile
    {
        public IndividualQueryExternalParamsEsProfile() : base()
        {
            CreateMap<IndividualQueryExternalParamsDTO, IndividualQueryExternalParamsEsDTO>().ReverseMap()
               .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.document, opt => opt.MapFrom(src => src.Identificacion))
               .ForMember(dest => dest.numberWords, opt => opt.MapFrom(src => src.CantidadPalabras))
               .ForMember(dest => dest.hasPriority4, opt => opt.MapFrom(src => src.TienePrioridad_4))
               .ForMember(dest => dest.typeDocument, opt => opt.MapFrom(src => src.TipoDocumento));
        }
    }
}
